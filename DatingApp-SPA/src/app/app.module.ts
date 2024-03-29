import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import { BsDropdownModule, TabsModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule} from 'ngx-gallery';
import { FileUploadModule} from 'ng2-file-upload';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { appRoutes } from '@./routes';
import { AuthGuard } from './_guards/auth.guard'
import { UserService } from './_services/user.service'
import { MessagesComponent } from './messages/messages.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard'
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';


export function tokenGetter(){
	return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
	  NavComponent,
	  HomeComponent,
	  RegisterComponent,
	  MemberListComponent,
      ListsComponent,
      MessagesComponent,
      MemberCardComponent,
	  MemberDetailComponent,
	  MemberEditComponent,
	  PhotoEditorComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
	  FormsModule,
	  BsDropdownModule.forRoot(),
	  TabsModule.forRoot(),
	  RouterModule.forRoot(appRoutes),
	  NgxGalleryModule,
	  FileUploadModule,
	  JwtModule.forRoot({
		  config: {
			  tokenGetter: tokenGetter,
			  whitelistedDomains: ['localhost:5000'],
			  blacklistedRoutes: ['localhost:5000/api/auth']
		  }
	  })
   ],
   providers: [
	  AuthService,
	  ErrorInterceptorProvider,
	  AlertifyService,
	  AuthGuard,
	  UserService,
	  MemberDetailResolver,
	  MemberListResolver,
	  MemberEditResolver,
	  PreventUnsavedChanges
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
