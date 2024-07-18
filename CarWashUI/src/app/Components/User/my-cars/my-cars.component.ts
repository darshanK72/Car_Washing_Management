import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Car } from 'src/app/Models/car.model';
import { AuthService } from 'src/app/Services/auth.service';
import { CarService } from 'src/app/Services/car.service';

@Component({
  selector: 'app-my-cars',
  templateUrl: './my-cars.component.html',
  styleUrls: ['./my-cars.component.css']
})
export class MyCarsComponent implements OnInit{
  cars: Car[] = [];
  carForm: FormGroup;
  userId!:number;

  constructor(private carService: CarService, private fb: FormBuilder,private authService:AuthService) {
    this.carForm = this.fb.group({
      make: ['', [Validators.required, Validators.maxLength(50)]],
      model: ['', [Validators.required, Validators.maxLength(50)]],
      year: ['', [Validators.required, Validators.min(1886), Validators.max(new Date().getFullYear())]],
      licensePlate: ['', [Validators.required, Validators.maxLength(20)]],
      imageUrl: ['', [Validators.required, Validators.pattern('(http(s?):)|([/|.|\w|\s])*\.(?:jpg|gif|png)')]],
      userId: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.loadCars();
    this.userId = this.authService.getUserId();
    this.carForm.patchValue({userId:this.userId});
  }

  loadCars(): void {
    this.carService.getAllCars().subscribe({
      next: (data) => this.cars = data,
      error: (error) => console.error('There was an error!', error)
    });
  }

  onSubmitCar(): void {
    if (this.carForm.valid) {
      const newCar: Car = this.carForm.value;
      this.carService.addCar(newCar).subscribe(() => {
        this.loadCars();
        this.carForm.reset();
      });
    } else {
      this.carForm.markAllAsTouched();
    }
  }
}
