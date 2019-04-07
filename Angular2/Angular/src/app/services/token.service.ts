import { Injectable } from '@angular/core';
import { JwtTokenPayload } from 'src/app/models/JwtTokenPayload';
import { fromEvent } from 'rxjs/internal/observable/fromEvent';
import { filter } from 'rxjs/internal/operators/filter';
import { map } from 'rxjs/internal/operators/map';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  public token = {
    payload: null as JwtTokenPayload,
    signature: null as string
  }

  constructor(private window: Window) {
    fromEvent(window, 'storage').pipe(
      filter(event => (event as StorageEvent).key == 'tokenSignature'),
      map(event => (event as StorageEvent).newValue)
    ).subscribe(newTokenSignature => {
      console.log('updated token in memory')
      this.token.signature = newTokenSignature;
    })
  }

  public KeepToken(jwtBase64String: string): void {
    this.token.signature = jwtBase64String;
    this.token.payload = this.createFromString(jwtBase64String);
    console.log('token string parsed and saved to local storage');
    console.log(this.token);
    this.window.localStorage.setItem('tokenSignature', jwtBase64String);
  }

  public createFromString(jwtBase64String: string): JwtTokenPayload {
    const base64payload = jwtBase64String.split('.')[1];
    const payload = this.window.atob(base64payload);
    const payloadObject = JSON.parse(payload);

    return <JwtTokenPayload> {
      subjectId: payloadObject.sub,
      issuedAtMs: +payloadObject.iat * 1000,
      expirationDateMs: +payloadObject.exp * 1000,
      issuer: payloadObject.iss,
      audience: payloadObject.aud,
      role: payloadObject['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    }
  }

  public isExpired(): boolean{
    if(!this.fetchToken().signature){
      return true;
    }

    return Date.now() > this.token.payload.expirationDateMs;
  }

  public fetchToken(): {payload: JwtTokenPayload, signature: string}{
    if(this.token.signature){
      return this.token;
    }

    const jwtBase64String = this.window.localStorage.getItem('tokenSignature');
  
    if(!jwtBase64String){
      return {} as any
    }

    this.token.payload = this.createFromString(jwtBase64String);
    this.token.signature = jwtBase64String;
    return this.token;
  }

  public clearToken(): void{
    this.token.payload = null;
    this.token.signature = null;
    this.window.localStorage.removeItem('tokenSignature');
  }
}
