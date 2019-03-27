import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes } from '@angular/router';
import { LoginComponent } from 'src/app/core/components/account/login/login.component';
import { RegisterComponent } from 'src/app/core/components/account/register/register.component';
import { RouterModule } from '@angular/router';

const accountRoutes: Routes = [
  { path: 'login',  component: LoginComponent },
  { path: 'register', component: RegisterComponent}
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(accountRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class AccountRoutingModule { }
