import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from './presentation/layouts/app/app-layout.component';
import { LoginComponent } from './presentation/shared/log-in/log-in.component';
import { AuthGuard } from './services/guards/permissions.service';
import { NotesComponent } from './presentation/pages/notes/notes.component';
import { MembersComponent } from './presentation/pages/members/members.component';
import { SettingsLayoutComponent } from './presentation/pages/settings/settings-layout.component';
import { AuthLayoutComponent } from './presentation/shared/auth-layout.component';
import { SignUpComponent } from './presentation/shared/sign-up/sign-up.component';

export const basePath = '';
export const loginPath = 'log-in';
export const signupPath = 'registration';
export const membersPath = 'members';
export const settingsPath = 'settings-layout';
export const notesPath = 'notes';


const routes: Routes = [
  {
    path: basePath, 
    component: AppLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: basePath,
        redirectTo: '/' + notesPath,
        pathMatch: 'full',
      },
      {
        path: notesPath,
        component: NotesComponent
      },
      {
        path: membersPath,
        component: MembersComponent,
      },
      {
        path: settingsPath,
        component: SettingsLayoutComponent,
        
      }
    ]
  },
  {
    path: basePath,
    component: AuthLayoutComponent,
    children: [
      {
        path: loginPath,
        component: LoginComponent,
      },
      {
        path: signupPath,
        component: SignUpComponent,
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
