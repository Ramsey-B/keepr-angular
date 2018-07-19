import { Component, OnInit } from '@angular/core';
import { User } from '../../models/User';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  user = {
    email: "",
    username: "",
    password: ""
  }
  currentUser: User

  constructor(private _accountService:AccountService) { }

  ngOnInit() {
    this._accountService.authenticate()
  }

  createUser() {
    this._accountService.registerUser(this.user);
  }

  loginUser() {
    this._accountService.loginUser(this.user);
  }
}
