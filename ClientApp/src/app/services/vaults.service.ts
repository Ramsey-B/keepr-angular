import { Injectable } from '@angular/core';
import { Vault } from '../models/Vault';
import { Http } from '@angular/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { BehaviorSubject } from "rxjs";
import { AccountService } from "./account.service";

@Injectable()
export class VaultsService {
  private baseUrl = "//localhost:5001/api/vault/";
  HttpOptions = {
    withCredentials: true,
    timeout: 3000
  };

  private vaults = new BehaviorSubject<Vault[]>(null);
  castVaults = this.vaults.asObservable();

  constructor(private http:Http, private _accountService:AccountService) {}

  getVaults(id) {
    return this.http.get(this.baseUrl + 'user/' + id, this.HttpOptions)
    .pipe(map(res => res.json())).subscribe(vaults => {
      this.vaults.next(vaults);
    })
  }

  createVault(vault) {
    this.http.post(this.baseUrl, vault, this.HttpOptions)
      .pipe(map(res => res.json())).subscribe(vault => {
        this.vaults.value.unshift(vault);
      })
  }
}
