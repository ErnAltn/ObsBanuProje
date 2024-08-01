import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LocalStorageService } from 'app/core/services/local-storage.service';
import { SharedService } from 'app/core/services/shared.service';
import { environment } from 'environments/environment';
import { UserService } from '../../user/services/user.service';
import { LoginUser } from '../model/login-user';
import { TokenModel } from '../model/token-model';


@Injectable({
  providedIn: 'root'
})

export class AuthService {

  userName: string;
  isLoggin: boolean;
  decodedToken: any;
  userToken: string;
  jwtHelper: JwtHelperService = new JwtHelperService();
  claims: string[];
  langText: string;
  userData: TokenModel;
  userId:string;
  token:string;


  constructor(private httpClient: HttpClient, private storageService: LocalStorageService,
    private router: Router, private alertifyService: AlertifyService, private sharedService: SharedService,private userService:UserService) {

    this.setClaims();
  }

 async login(loginUser: LoginUser) {

    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json")

    this.httpClient.post<TokenModel>(environment.getApiUrl + "/Auth/login", loginUser, { headers: headers }).subscribe(data => {


      if (data.success) {
        this.storageService.setToken(data.data.token);
        this.storageService.setItem("refreshToken", data.data.refreshToken)
        this.claims = data.data.claims;
        console.log(this.claims);
        this.userData = data;
        this.token=data.data.token;
        var decode = this.jwtHelper.decodeToken(this.storageService.getToken());
        this.userId= this.userData.data.userId.toString();
        this.storageService.setItem("userId",this.userId);
        var propUserName = Object.keys(decode).filter(x => x.endsWith("/name"))[0];
        this.userName = decode[propUserName];
        this.sharedService.sendChangeUserNameEvent();

        this.router.navigateByUrl("/dashboard");
        if (this.langText == null || this.langText == "tr-TR")
          this.alertifyService.success("Başarıyla Giriş Yapıldı!")
        else {
          this.alertifyService.success("Successfully Login!!")
        }
      }
    }
    );
    await this.delay(500);
    if (this.userData == null) {
      if (this.langText == "tr-TR" || this.langText == null) {
        this.alertifyService.error("Kullanıcı adı veya parola hatalı!");
      } else {
        this.alertifyService.error("Wrong username or password!");
      }
    }
  }

  delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
  getUserName(): string {
    return this.userName;
  }
 
   getToken(): string {
    return this.token;
  }

  getClaims(){
    console.log(this.claims);
  }

  setClaims() {

    if ((this.claims == undefined || this.claims.length == 0) && this.storageService.getToken() != null && this.loggedIn()) {

      this.httpClient.get<string[]>(environment.getApiUrl + "/operation-claims/cache").subscribe(data => {
        this.claims = data;
      })


      var token = this.storageService.getToken();
      var decode = this.jwtHelper.decodeToken(token);

      var propUserName = Object.keys(decode).filter(x => x.endsWith("/name"))[0];
      this.userName = decode[propUserName];
    }
  }

  logOut() {
    this.storageService.removeToken();
    this.storageService.removeItem("lang")
    this.storageService.removeItem("refreshToken");
    this.storageService.removeItem("userId");
    this.claims = [];
  }

  loggedIn(): boolean {

    let isExpired = this.jwtHelper.isTokenExpired(this.storageService.getToken(), -120);
    return !isExpired;
  }

  getCurrentUserId() {
  this.jwtHelper.decodeToken(this.storageService.getToken()).userId;
  }
getUserClaims(){
  
}
  claimGuard(claim: string): boolean {
    if (!this.loggedIn())
      this.router.navigate(["/login"]);

    var check = this.claims.some(function (item) {
      return item == claim;
    })

    return check;
  }

}
