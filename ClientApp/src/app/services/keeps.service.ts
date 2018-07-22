import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { BehaviorSubject } from "rxjs";
import { Keep } from '../models/Keep';
import { AccountService } from './account.service';

@Injectable()
export class KeepsService {

  private baseUrl = "//localhost:5001/api/keep/";
  HttpOptions = {
    withCredentials: true,
    timeout: 3000
  };

  private keeps = new BehaviorSubject<Keep[]>(null);
  castKeeps = this.keeps.asObservable();

  constructor(private http: Http, private _router:Router, private _accountService:AccountService) { }

  getKeeps() {
    this.http.get(this.baseUrl, this.HttpOptions)
    .pipe(map(res => res.json())).subscribe(keeps => {
      this.keeps.next(keeps);
    })
  }

  getKeepsByAuthorId(id) {
    this.http.get(this.baseUrl + 'user/' + id, this.HttpOptions)
      .pipe(map(res => res.json())).subscribe(keeps => {
        this.keeps.next(keeps);
      })
  }

  createKeep(newKeep) {
    this.http.post(this.baseUrl, newKeep, this.HttpOptions) 
      .pipe(map(res => res.json())).subscribe(keep => {
        this.keeps.value.unshift(keep);
      })
  }
}
