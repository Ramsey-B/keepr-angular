import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from 'rxjs/operators';
import { Tag } from '../models/Tag';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class TagService {
  private baseUrl = "//localhost:5001/api/tag/";
  HttpOptions = {
    withCredentials: true,
    timeout: 3000
  };

  private tagsSource = new BehaviorSubject<Tag[]>([]);
  tags = this.tagsSource.asObservable();

  constructor(private http:Http) { }

  createTags(keepId:number, tags:Tag[]) {
    this.http.post(this.baseUrl + keepId, tags, this.HttpOptions)
      .pipe(map(res => res.json())).subscribe(tags => {
        this.tagsSource.next(tags);
      })
  }
}
