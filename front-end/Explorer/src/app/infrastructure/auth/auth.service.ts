import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { TokenStorage } from './jwt/token.service';
import { environment } from 'src/env/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Login } from './model/login.model';
import { AuthenticationResponse } from './model/authentication-response.model';
import { User } from './model/user.model';
import { Registration } from './model/registration.model';
import { ShoppingCart } from 'src/app/feature-modules/marketplace/model/shopping-cart.model';
import { Wallet } from 'src/app/feature-modules/marketplace/model/wallet.model';
import { PasswordChange } from './model/password-change.model';
import { AuthenticationData } from './model/authentication-data.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  user$ = new BehaviorSubject<User>({username: "", id: 0, role: "", isBlogEnabled: false });

  constructor(private http: HttpClient,
    private tokenStorage: TokenStorage,
    private router: Router) { }

  login(login: Login): Observable<AuthenticationData> {
    return this.http
      .post<AuthenticationData>(environment.apiHost + 'users/login', login)
      .pipe(
        tap((authenticationResponse) => {
          console.log(authenticationResponse.jwtToken)
          this.tokenStorage.saveAccessToken(authenticationResponse.jwtToken);
          this.setUser();
        })
      );
  }

  register(registration: Registration): Observable<AuthenticationData> {
    return this.http
    .post<AuthenticationData>(environment.apiHost + 'users', registration)
    .pipe(
      tap((authenticationResponse) => {
        this.tokenStorage.saveAccessToken(authenticationResponse.jwtToken);
        //this.setUser();
      })
    );
  }

  logout(): void {
    this.router.navigate(['/home']).then(_ => {
      this.tokenStorage.clear();
      this.user$.next({username: "", id: 0, role: "" });
      }
    );
  }

  sendPasswordResetLink(email: string): Observable<boolean>{
      return this.http.get<boolean>(environment.apiHost + `users/forgotPassword?email=` + email)
  }

  changePassword(change: PasswordChange): Observable<boolean>{
    return this.http.post<boolean>(environment.apiHost + `users/changePassword`, change);
  }

  checkIfUserExists(): void {
    const accessToken = this.tokenStorage.getAccessToken();
    if (accessToken == null) {
      return;
    }
    this.setUser();
  }
  activateUser(id: number): Observable<boolean> {
    const credentials : Login =  {
      username: "",
      password: ""
    }
    return this.http.patch<boolean>(environment.apiHost + `users/activate/` + id, credentials);
  }

  createShoppingCart(shoppingCart: ShoppingCart): Observable<ShoppingCart> {
    return this.http.post<ShoppingCart>(environment.apiHost + 'tourist/order', shoppingCart);
  }

  createWallet(wallet: Wallet):Observable<Wallet> {
    return this.http.post<Wallet>(environment.apiHost + 'tourist/wallet', wallet);
  }

  private setUser(): void {
    const jwtHelperService = new JwtHelperService();
    const accessToken = this.tokenStorage.getAccessToken() || "";
    const user: User = {
      id: +jwtHelperService.decodeToken(accessToken).id,
      username: jwtHelperService.decodeToken(accessToken).username,
      /*role: jwtHelperService.decodeToken(accessToken)[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ],*/
      role: jwtHelperService.decodeToken(accessToken).role,
    };
    console.log(this.user$)
    this.user$.next(user);
  }
}
