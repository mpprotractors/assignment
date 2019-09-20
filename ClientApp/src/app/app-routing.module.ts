import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DependencyComponent } from './dependency/dependency.component';


const routes: Routes = [
  { path: 'dependencies', component: DependencyComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
