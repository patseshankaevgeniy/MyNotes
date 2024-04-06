import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from './pages/app-layout.component';
import { SignInComponent } from './pages/auth/sign-in/sign-in.component';

export const basePath = '';
export const signInPath = 'login';


const routes: Routes = [
  {
    path: basePath, 
    component: AppLayoutComponent,
    children: [
      {
        path: signInPath,
        component: SignInComponent
      }]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
