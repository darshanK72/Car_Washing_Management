import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Package } from 'src/app/Models/package.model';
import { AdminService } from 'src/app/Services/admin.service';
import { PackageService } from 'src/app/Services/package.service';

@Component({
  selector: 'app-add-package',
  templateUrl: './add-package.component.html',
  styleUrls: ['./add-package.component.css']
})
export class AddPackageComponent implements OnInit {
  packages: Package[] = [];
  packageForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private packageService: PackageService
  ) {
    this.packageForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(200)]],
      price: ['', [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    this.loadPackages();
  }

  loadPackages(): void {
    this.packageService.getAllPackages().subscribe(
      (packages: Package[]) => this.packages = packages,
      error => console.error('Error loading packages', error)
    );
  }

  addPackage(): void {
    if (this.packageForm.valid) {
      this.packageService.createPackage(this.packageForm.value).subscribe(
        response => {
          this.packages.push(response);
          this.packageForm.reset();
          console.log('Package added', response);
        },
        error => console.error('Error adding package', error)
      );
    }
  }

  deletePackage(packageId: number): void {
    this.packageService.deletePackage(packageId).subscribe(
      response => {
        this.packages = this.packages.filter(pkg => pkg.packageId !== packageId);
        console.log('Package deleted', response);
      },
      error => console.error('Error deleting package', error)
    );
  }
}
