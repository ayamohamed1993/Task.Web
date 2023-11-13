import { AuthResponseDto } from '../../Models/response/authResponseDto.model';
import { Injectable } from '@angular/core';
import { UserForRegistrationDto } from '../../Models/user/userForRegistrationDto.model'; 
import { RegistrationResponseDto } from '../../Models/response/registrationResponseDto.model';
import { HttpClient } from '@angular/common/http';
import { UserForAuthenticationDto } from '../../Models/user/userForAuthenticationDto.model';
import { Subject } from 'rxjs';
import { environment } from '../../environments/environment';


export class AuthenticationService {
  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();
  public urlAddress: string = "";//environment.urlAddress;

  constructor(private http: HttpClient) { }

  public registerUser = (route: string, body: UserForRegistrationDto) => {
    return this.http.post<RegistrationResponseDto> (this.createCompleteRoute(route, this.urlAddress), body);
  }

  public loginUser = (route: string, body: UserForAuthenticationDto) => {
    return this.http.post<AuthResponseDto>(this.createCompleteRoute(route, this.urlAddress), body);
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  public logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
