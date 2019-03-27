import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes } from '@angular/router';
import { RouterModule } from '@angular/router';
import { PageNotFoundComponent } from 'src/app/core/components/page-not-found/page-not-found.component';
import { ProjectsModule } from 'src/app/core/components/projects/projects.module';

const appRoutes: Routes = [
  { path: 'projects',loadChildren: "src/app/core/components/projects/projects.module#ProjectsModule" },
  { path: 'account', loadChildren: 'src/app/core/components/account/account.module#AccountModule' },
  { path: '', redirectTo: '/projects', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ],

  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }