import { Component, OnInit } from '@angular/core';
import { KeepsService } from '../../services/keeps.service';
import { AccountService } from '../../services/account.service';
import { Router } from '@angular/router';
import { User } from '../../models/User';
import { Keep } from '../../models/Keep';
import { Tag } from '../../models/Tag';

@Component({
  selector: 'app-keeps',
  templateUrl: './keeps.component.html',
  styleUrls: ['./keeps.component.css']
})
export class KeepsComponent implements OnInit {
  private user: User
  private keeps: Keep[]
  tags: Tag[]
  tag = {
    tagName: ''
  }
  newKeep = {
    img: '',
    description: ''
  }

  constructor(private _keepsService: KeepsService, private _accountService: AccountService, private _router: Router) { }

  ngOnInit() {
    this._accountService.castUser.subscribe(user => {
      if (user) {
        this.user = user;
      } else {
        this._router.navigate(['account'])
      }
    })
    if (this.user) {
      this._keepsService.getKeepsByAuthorId(this.user.id)
    }
    this._keepsService.castKeeps.subscribe(keeps => {
      this.keeps = keeps;
    })
  }

  createKeep() {
    this._keepsService.createKeep(this.newKeep)
  }

  addTag() {

  }

  removeTag(i) {

  }
}
