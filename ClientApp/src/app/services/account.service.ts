import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { User, CreateUser } from '../models/User';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { BehaviorSubject } from "rxjs";

@Injectable()
export class AccountService {

  private baseUrl = "//localhost:5000/api/account";
  HttpOptions = {
    withCredentials: true,
    timeout: 3000
  };

  private user = new BehaviorSubject<User>(null);
  castUser = this.user.asObservable();

  constructor(private http: Http, private _router:Router) { }

  authenticate() {
    this.http.get(this.baseUrl + "/authenticate", this.HttpOptions)
      .pipe(map(res => res.json())).subscribe(u => {
        if (u != null) {
          this.user = u;
          this._router.navigate(['/']);
        } else {
          this._router.navigate(['account']);
        }
      })
  }

  registerUser(newUser:Object){
    this.http.post(this.baseUrl + '/register', newUser, this.HttpOptions)
      .pipe(map(res => res.json())).subscribe(user => {
        this.user = user;
      })
  }

  loginUser(user:Object){
    this.http.post(this.baseUrl + '/login', user, this.HttpOptions)
      .pipe(map(res => res.json())).subscribe(u => {
        debugger
        this.user = u;
      })
  }
}
