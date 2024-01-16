import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-ui-component',
  standalone: true,
  imports: [FormsModule, HttpClientModule, MatIconModule, CommonModule],
  templateUrl: './ui-component.component.html',
  styleUrl: './ui-component.component.scss'
})
export class UiComponentComponent {



  
  numberOfDays: number | null = null;
  fileName = '';
  files: File[] = []

  trainingFile!: File;

  trainingStartDate!:Date;
  trainingEndDate!:Date;
  resultStartDate!:Date;
  constructor(private httpClient: HttpClient) { }



  onFileInput(event: any) {

    for (let i = 0; i < event.target.files.length; i++) {
      this.files.push(event.target.files[i]);
    }
  }

  onFileInputTrain(event: any){
      this.trainingFile= event.target.files[0];
  }


  trainWithData(): void {
    // Assuming your C# endpoint for training is 'your-csharp-backend-url/train'
    this.httpClient.get(`https://localhost:7058/?startDate=${this.trainingStartDate}&endDate=${this.trainingEndDate}&`).subscribe(
      (response: any) => {
        console.log('Training successful', response);
      },
      (error: any) => {
        console.error('Error training with data', error);
      }
    );
  }

  showResults(): void {
    // Assuming your C# endpoint for showing results is 'your-csharp-backend-url/results'
    this.httpClient.get(`your-csharp-backend-url/results?startDate=${this.numberOfDays}`).subscribe(
      (response: any) => {
        console.log('Results:', response);
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
    if (this.files) {

      const formData = new FormData();

      for (let i = 0; i < this.files.length; i++) {
        formData.append("csvFile", this.files[i], this.files[i].name);
      }


      const upload$ = this.httpClient.post("https://localhost:7058/UploadData", formData);

      upload$.subscribe();
    }
  }
}


