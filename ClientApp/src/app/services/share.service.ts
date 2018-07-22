import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';
import { Keep } from '../models/Keep';

@Injectable()
export class ShareService {
  private baseUrl = "//localhost:5001/api/share/";
  HttpOptions = {
    withCredentials: true,
    timeout: 3000
  };

  private keepsSource = new BehaviorSubject<Keep[]>([])
  keeps = this.keepsSource.asObservable();

  constructor(private http:Http) { }

  getSharesByVaultId(vaultId:number) {
    this.http.get(this.baseUrl + 'vault/' +vaultId, this.HttpOptions)
      .pipe(map(res => res.json())).subscribe(keeps => {
        this.keepsSource.next(keeps);
      })
  }

  getSharesByAuthorId(authorId:string) {
    this.http.get(this.baseUrl + 'user/' + authorId, this.HttpOptions)
      .pipe(map(res => res.json())).subscribe(keeps => {
        console.log(keeps)
        this.keepsSource.next(keeps);
      })
  }
}
