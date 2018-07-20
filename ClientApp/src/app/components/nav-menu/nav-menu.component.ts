import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { User } from '../../models/User';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  user: User;

  constructor(private _router: Router, private _accountService:AccountService) { }
  
  ngOnInit() {
    this._accountService.castUser.subscribe(user => {
      console.log(user)
      this.user = user;
    })
  }

  signOut() {
    this._accountService.signOut(this.user.id);
  }
}
