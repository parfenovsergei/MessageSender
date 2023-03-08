import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { Routes, RouterModule } from '@angular/router';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { MessageComponent } from './components/message/message-create/message.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { NgxMatDatetimePickerModule, NgxMatNativeDateModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { MessagesViewComponent } from './components/message/messages-view/messages-view.component';
import { MatDividerModule } from '@angular/material/divider';
import { FlexLayoutModule } from '@angular/flex-layout';
import { JwtModule } from '@auth0/angular-jwt';
import { MessageEditComponent } from './components/message/message-edit/message-edit.component';

const appRoutes: Routes =[
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'message', component: MessageComponent},
  { path: 'messages', component: MessagesViewComponent},
  { path: 'message/:id', component: MessageEditComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    ToolbarComponent,
    LoginComponent,
    RegisterComponent,
    MessageComponent,
    MessagesViewComponent,
    MessageEditComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(appRoutes),
    MatToolbarModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule, 
    ReactiveFormsModule,
    MatCardModule,
    HttpClientModule,
    MatSnackBarModule,
    NgxMatDatetimePickerModule,
    NgxMatTimepickerModule,
    NgxMatNativeDateModule,
    MatDatepickerModule, 
    MatDividerModule,
    FlexLayoutModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: request => request as any
      }
    })
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
