import {  Routes } from '@angular/router';
import { UsersigninComponent } from './Pages/user/usersignin/usersignin.component';
import { UsersignupComponent } from './Pages/user/usersignup/usersignup.component';
import { AppComponent } from './app.component';
import { WelcomepageComponent } from './Pages/welcomepage/welcomepage/welcomepage.component';
import { AdministratorloginComponent } from './Pages/administrator/administratorlogin/administratorlogin.component';
import { TrainersigninComponent } from './Pages/trainer/trainersignin/trainersignin.component';
import { TrainersignupComponent } from './Pages/trainer/trainersignup/trainersignup.component';
import { NavigationpageComponent } from './Pages/homepage/navigationpage/navigationpage.component';
import { LandingPageComponent } from './Pages/administrator/landing-page/landing-page.component';
import { UserlandingPageComponent } from './Pages/user/userlanding-page/userlanding-page.component';
import { TrainerapproveComponent } from './Pages/administrator/trainerapprove/trainerapprove.component';
import { TrainerviewComponent } from './Pages/administrator/trainerview/trainerview.component';
import { ContactComponent } from './Pages/homepage/navigationpage/contact/contact.component';
import { AboutUsComponent } from './Pages/homepage/navigationpage/about-us/about-us.component';
import { TrainerlandingpageComponent } from './Pages/trainer/trainerlandingpage/trainerlandingpage.component';
import { authGuard } from './guards/auth-guard.guard';
import { AddcourseComponent } from './Pages/trainer/addcourse/addcourse.component';
import { ViewcourseComponent } from './Pages/trainer/viewcourse/viewcourse.component';
import { AddmoduleComponent } from './Pages/trainer/addmodule/addmodule.component';
import { ViewmoduleComponent } from './Pages/trainer/viewmodule/viewmodule.component';
import { AddassignmentquestionComponent } from './Pages/trainer/addassignmentquestion/addassignmentquestion.component';
import { SinglemoduleComponent } from './Pages/trainer/singlemodule/singlemodule.component';
import { ViewcoursesComponent } from './Pages/user/viewcourses/viewcourses.component';
import { ViewmodulesComponent } from './Pages/user/viewmodules/viewmodules.component';
import { SinglemodulesComponent } from './Pages/user/singlemodules/singlemodules.component';
import { ReviewassignmentComponent } from './Pages/trainer/reviewassignment/reviewassignment.component';
import { AssignmentsubmittedComponent } from './Pages/user/assignmentsubmitted/assignmentsubmitted.component';
import { NoteByUserIdComponent } from './Pages/Notes/note-by-user-id/note-by-user-id.component';
import { NewNotesComponent } from './Pages/Notes/new-notes/new-notes.component';
import { UpdateNotesComponent } from './Pages/Notes/update-notes/update-notes.component';
import { DeleteNotesComponent } from './Pages/Notes/delete-notes/delete-notes.component';


export const routes: Routes = [
    {
      path: '',
      redirectTo: 'welcome',
      pathMatch: 'full'
    },
    { 
      path: 'welcome',
      component: WelcomepageComponent 
    },
    { 
      path: 'login',
      component: UsersigninComponent 
    },
    { 
      path: 'administrator',
      component: AdministratorloginComponent
    },
    { 
      path: 'trainer-signin',
      component: TrainersigninComponent
    },
    { 
      path: 'trainer-signup',
      component: TrainersignupComponent
    },
    {
      path: 'admin-landingpage',
      component: LandingPageComponent,
      canActivate: [authGuard],  // Use the authGuard here for admins
      data: { role: 'ADMIN' }  // This route is for Admin users only
    },
    {
      path: 'admin-trainer-approve',
      component: TrainerapproveComponent,
      canActivate: [authGuard],  // Use the authGuard here for admins
      data: { role: 'ADMIN' }  // This route is for Admin users only
    },
    {
      path: 'user-landingpage',
      component: UserlandingPageComponent,
      canActivate: [authGuard],  // Use the authGuard here for users
      data: { role: 'LEARNER' }  // This route is for User role only
    },
    { 
      path: 'trainer-details/:id', 
      component: TrainerviewComponent,
      canActivate:[authGuard],
      data:{role:'ADMIN'}
    },
    { 
      path: 'contact', 
      component: ContactComponent
    },
    { 
      path: 'aboutus', 
      component: AboutUsComponent
    },
    { 
      path: 'trainerlandingpage', 
      component: TrainerlandingpageComponent,
      canActivate: [authGuard],  // Use the authGuard here for trainers
      data: { role: 'TRAINER' }  // This route is for Trainer users only
    },
    { 
      path: 'addcourse', 
      component: AddcourseComponent,
      canActivate: [authGuard],  // Use the authGuard here for trainers
      data: { role: 'TRAINER' }  // This route is for Trainer users only
    },
    { 
      path: 'viewcourse', 
      component: ViewcourseComponent,
      canActivate: [authGuard],  // Use the authGuard here for trainers
      data: { role: 'TRAINER' }  // This route is for Trainer users only
    },
    { 
      path: 'addmodule', 
      component: AddmoduleComponent,
      canActivate: [authGuard],  // Use the authGuard here for trainers
      data: { role: 'TRAINER' }  // This route is for Trainer users only
    },
    { 
      path: 'trainer/modules/:courseId', 
      component: ViewmoduleComponent,
      canActivate: [authGuard],  // Use the authGuard here for trainers
      data: { role: 'TRAINER' }  // This route is for Trainer users only
    },
    { 
      path: 'addassignmentquestion', 
      component: AddassignmentquestionComponent,
      canActivate: [authGuard],  // Use the authGuard here for trainers
      data: { role: 'TRAINER' }  // This route is for Trainer users only
    },
    { 
      path: 'singlemodule/:id', 
      component: SinglemoduleComponent,
      canActivate: [authGuard],  // Use the authGuard here for trainers
      data: { role: 'TRAINER' }  // This route is for Trainer users only
    },
    {
      path: 'viewcourses',
      component: ViewcoursesComponent,
      canActivate: [authGuard],  // Use the authGuard here for users
      data: { role: 'LEARNER' }  // This route is for User role only
    },
    {
      path: 'usermodule/:courseId',
      component: ViewmodulesComponent,
      canActivate: [authGuard],  // Use the authGuard here for users
      data: { role: 'LEARNER' }  // This route is for User role only
    },
    {
      path: 'usersinglemodules/:moduleId',
      component: SinglemodulesComponent,
      canActivate: [authGuard],
      data: { role: 'LEARNER' }
    },
    { 
      path: 'reviewassignment', 
      component: ReviewassignmentComponent,
      canActivate: [authGuard],  // Use the authGuard here for trainers
      data: { role: 'TRAINER' }  // This route is for Trainer users only
    },
    {
      path: 'submittedassignment',
      component: AssignmentsubmittedComponent,
      canActivate: [authGuard],
      data: { role: 'LEARNER' }
    },{
      path: 'notes',
      component: NoteByUserIdComponent,
      canActivate: [authGuard],  // Use the authGuard here for users
      data: { role: 'LEARNER' }  // This route is for User role only
    },
    {
      path: 'new-notes',
      component: NewNotesComponent,
      canActivate: [authGuard],  // Use the authGuard here for users
      data: { role: 'LEARNER' }  // This route is for User role only
    },
    {
      path: 'update-notes',
      component: UpdateNotesComponent,
      canActivate: [authGuard],  // Use the authGuard here for users
      data: { role: 'LEARNER' }  // This route is for User role only
    },
    {
      path: 'delete-notes',
      component: DeleteNotesComponent,
      canActivate: [authGuard],  // Use the authGuard here for users
      data: { role: 'LEARNER' }  // This route is for User role only
    },
  ];
