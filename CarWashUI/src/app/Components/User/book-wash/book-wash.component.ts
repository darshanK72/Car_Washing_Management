import { Element } from '@angular/compiler';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { AuthService } from 'src/app/Services/auth.service';
import { CarService } from 'src/app/Services/car.service';
import { OrderService } from 'src/app/Services/order.service';
import { PackageService } from 'src/app/Services/package.service';

@Component({
  selector: 'app-book-wash',
  templateUrl: './book-wash.component.html',
  styleUrls: ['./book-wash.component.css']
})
export class BookWashComponent {

  @ViewChild('toast') toastEl!: ElementRef;

  toastMessage: string = '';
  isSuccess: boolean = true;
  
  orderForm!: FormGroup;
  packages: any[] = [];
  cars: any[] = [];
  userId!:number;
  
  constructor(
    private fb: FormBuilder,
    private authService:AuthService,
    private orderService: OrderService,
    private packageService: PackageService,
    private carService:CarService
  ) { }

  ngOnInit() {
    this.loadPackages();
    this.userId = this.authService.getUserId();
    this.loadUserCars();
    this.initForm();
  }

  initForm() {
    this.orderForm = this.fb.group({
      userId: [this.userId, Validators.required],
      carId: [null, Validators.required],
      packageId: [null, Validators.required],
      scheduleNow: [true, Validators.required],
      scheduledDate: [null],
      notes: ['', Validators.maxLength(500)]
    });

    this.orderForm.get('scheduleNow')?.valueChanges.subscribe(scheduleNow => {
      const scheduledDateControl = this.orderForm?.get('scheduledDate');
      if (!scheduleNow) {
        scheduledDateControl?.setValidators([Validators.required]);
      } else {
        scheduledDateControl?.clearValidators();
      }
      scheduledDateControl?.updateValueAndValidity();
    });
  }

  loadPackages() {
    this.packageService.getAllPackages().subscribe(
      
      packages => {
        this.packages = packages;
        console.log(this.packages);
      },
      error => console.error('Error loading packages', error)
    );
  }

  loadUserCars() {
    this.carService.getCarByUserId(this.userId).subscribe(
      cars => {
        console.log(cars);
        this.cars = cars;
      },
      error => console.error('Error loading user cars', error)
    );
  }

  onSubmit() {
    console.log(this.orderForm);
    if (this.orderForm.valid) {
      this.orderService.placeOrder(this.orderForm.value).subscribe(
        response => {
          window.alert('Order placed successfully');
          this.orderForm.reset();
        },
        error => {
          window.alert('Error placing order' + error.error);
        }
      );
    } else {
      Object.keys(this.orderForm.controls).forEach(key => {
        const control = this.orderForm.get(key);
        control?.markAsTouched();
      });
    }
  }
}