import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { saveAs } from 'file-saver';
@Component({
  selector: 'app-ui-component',
  standalone: true,
  imports: [FormsModule, HttpClientModule, MatIconModule, CommonModule],
  templateUrl: './ui-component.component.html',
  styleUrl: './ui-component.component.scss'
})
export class UiComponentComponent {




  numberOfDays: number = 1;
  fileName = '';
  files: File[] = []

  trainingFile!: File;

  trainingStartDate: Date = new Date();
  trainingEndDate: Date = new Date();
  resultStartDate: Date = new Date();
  constructor(private httpClient: HttpClient) { }



  onFileInput(event: any) {

    for (let i = 0; i < event.target.files.length; i++) {
      this.files.push(event.target.files[i]);
    }
  }

  onFileInputTrain(event: any) {
    this.trainingFile = event.target.files[0];
  }


  trainWithData() {
    console.log(this.trainingStartDate);
    const params = new HttpParams()
    .append('startDate', new Date(this.trainingStartDate).toISOString())
    .append('endDate', new Date(this.trainingEndDate).toISOString());

    this.httpClient.get('https://localhost:7058/TrainWithData',{ params: params}).
      subscribe(response => {
        
        console.log("Success");
      });
  }


showResults(): void {

  const params = new HttpParams()
    .append('startDate', new Date(this.resultStartDate).toISOString())
    .append('noOfDays', this.numberOfDays.toString());

    const formData = new FormData();
    formData.append("Model",this.trainingFile);
  // Assuming your C# endpoint for showing results is 'your-csharp-backend-url/results'
  this.httpClient.get('https://localhost:7058/GetResult',{params : params, responseType: 'blob'}).subscribe(
    (response: any) => {
      const blob = new Blob([response], { type: 'text/csv' });
        const link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = 'predictionData.csv';
        link.click();
    },
    (error: any) => { 
      console.error('Error fetching results', error);
    }
  );
}

onNumberOfDaysChange(): void {
  // Handle changes to the 'numberOfDays' input here if needed
}


uploadData(): void {
  if(this.files) {

  const formData = new FormData();

  for (let i = 0; i < this.files.length; i++) {
    formData.append("csvFile", this.files[i], this.files[i].name);
  }


  const upload$ = this.httpClient.post("https://localhost:7058/UploadData", formData);

  upload$.subscribe();
}
  }

downloadModelFile(modelBlob: Blob): void {
  const blob = new Blob([modelBlob], { type: 'application/json' });
  const link = document.createElement('a');
  link.href = window.URL.createObjectURL(blob);
  link.download = 'TrainingModel.json';
  link.click();
}
}


