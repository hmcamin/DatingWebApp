import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';

@NgModule({
   declarations: [
      AppComponent,
      ValueComponent,
	  NavComponent,
	  HomeComponent,
	  RegisterComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
	  FormsModule
   ],
   providers: [
	  AuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
