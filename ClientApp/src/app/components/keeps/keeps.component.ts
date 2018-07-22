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
  tags = []
  tag = {
    tagName: '',
    id: 0,
    keepId: 0,
    authorId: ''
  }
  newKeep = {
    img: '',
    description: '',
    tags: [],
    authorId: '',
    author: '',
    id: 0,
    views: 0,
    keeps: 0,
    publicPrivate: false
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
    this.newKeep.authorId = this.user.id
    this.newKeep.author = this.user.username;
    this._keepsService.createKeep(this.newKeep, this.tags)
  }

  addTag() {
    var newTag = {
      tagName: this.tag.tagName,
      id: 0,
      keepId: 0,
      authorId: this.user.id
    }
    this.tags.unshift(newTag)
    this.tag.tagName = ''
  }

  removeTag(i) {
    this.tags.splice(i, 1);
  }
}
