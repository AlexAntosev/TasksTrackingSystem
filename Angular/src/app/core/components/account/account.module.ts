import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AccountRoutingModule } from 'src/app/core/components/account/account-routing/account-routing.module';
import { AccountComponent } from 'src/app/core/components/account/account.component';
import { LoginComponent } from 'src/app/core/components/account/login/login.component';
import { RegisterComponent } from 'src/app/core/components/account/register/register.component';

@NgModule({
  declarations: [
    AccountComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    AccountRoutingModule
  ],

  exports: [
    AccountComponent
  ]
})
export class AccountModule { }
