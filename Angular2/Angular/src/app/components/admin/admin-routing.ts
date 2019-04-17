import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdminGuard } from 'src/app/core/guard/admin.guard';

const routes: Routes = [
  
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ]
})
export class AdminRoutingModule {

}
