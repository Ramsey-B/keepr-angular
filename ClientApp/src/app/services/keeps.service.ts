import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { BehaviorSubject } from "rxjs";
import { Keep } from '../models/Keep';

@Injectable()
export class KeepsService {

  private baseUrl = "//localhost:5001/api/keep/";
  HttpOptions = {
    withCredentials: true,
    timeout: 3000
  };

  private keeps = new BehaviorSubject<Keep>(null);
  castKeeps = this.keeps.asObservable();

  constructor(private http: Http, private _router:Router) { }

  getKeeps() {
    this.http.get(this.baseUrl, this.HttpOptions)
    .pipe(map(res => res.json())).subscribe(keeps => {
      this.keeps.next(keeps)
    })
  }
}
