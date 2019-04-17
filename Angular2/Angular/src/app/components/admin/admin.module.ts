import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing';
import { UsersForAdminComponent } from 'src/app/components/admin/users/users.component';

@NgModule({
  imports: [
    CommonModule,
    AdminRoutingModule
  ],
  declarations: [UsersForAdminComponent]
})
export class AdminModule { }
