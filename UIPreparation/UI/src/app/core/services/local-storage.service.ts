import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  setToken(token: string) {
    localStorage.setItem("token", token);
  }
  
  setUserId(userId:string){
  localStorage.setItem("userId",userId)
  }

  removeToken(){
    localStorage.removeItem("token");
  }

  removeItem(itemName:string)
  {
    localStorage.removeItem(itemName);
  }

  getToken():string {
    return localStorage.getItem("token");
  }
  getUserId():number{
     return parseInt(localStorage.getItem("userId"),10) 
  }
 
  setItem(key:string,data:any){
    localStorage.setItem(key,data);
  }

  getItem(key:string){
    return localStorage.getItem(key);
  }

}
