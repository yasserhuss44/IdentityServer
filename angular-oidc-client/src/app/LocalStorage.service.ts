import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class LocalStorageService {
  public getItem(name: string) {
    const item = localStorage.getItem(name);
    if (item) return JSON.parse(item);
    return '';
  }

  public deleteItem(key: string) {
    localStorage.removeItem(key);
  }

  public setItem(name: string, value: any, callBack = null) {
    localStorage.setItem(name, JSON.stringify(value));
  }
  constructor() {}
}
