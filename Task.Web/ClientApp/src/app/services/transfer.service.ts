import { AuthResponseDto } from '../../Models/response/authResponseDto.model';
import { Injectable } from '@angular/core';
import { UserForRegistrationDto } from '../../Models/user/userForRegistrationDto.model'; 
import { RegistrationResponseDto } from '../../Models/response/registrationResponseDto.model';
import { HttpClient } from '@angular/common/http';
import { UserForAuthenticationDto } from '../../Models/user/userForAuthenticationDto.model';
import { Subject } from 'rxjs';
import { environment } from '../../environments/environment';


export class TransferService {
  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();
  public urlAddress: string = "";//environment.urlAddress;

  constructor(private http: HttpClient) { }

  public tranferBalance = (route: string, body: TransferDto) => {
    return this.http.post<any> (this.createCompleteRoute(route, this.urlAddress), body);
  }
  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
  
}
