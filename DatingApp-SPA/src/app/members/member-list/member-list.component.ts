import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { User } from '../../_models/user';
import { Pagination, PaginatedResult } from '../../_models/pagination';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent {
	users: User[];
    constructor(
		private userService: UserService,
		private alertifyService: AlertifyService) { }

	ngOnInit() {
		this.loadUsers();
	});
	loadUsers(){
		this.userService.getUsers().subscribe((users: User[]) =>{
			this.users = users;
		}, error => {
			this.alertify.error(error);
		});
	}
}
