import { Cliente } from './../../../models/cliente/cliente';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { APIResponse } from '../../../models/common/apiResponse';

@Injectable({
  providedIn: 'root'
})

export class ClienteService {

  constructor(@Inject('BASE_URL') private url: string, private httpClient: HttpClient) { }

  public createCliente(cliente: Cliente): Observable<APIResponse<boolean>> {
    return this.httpClient.post<APIResponse<boolean>>(this.url + 'Cliente/CreateCustomer', cliente)
  }

  public getCurrentCustomer() {
    return this.httpClient.get<APIResponse<any>>(this.url + 'Cliente/GetCurrentCustomer')
  }

  public getInvoicesByCustomer() {
    return this.httpClient.get<APIResponse<any>>(this.url + 'Invoice/InvoicesHistory')
  }

  public getInvoiceById(id: string): any {
    return this.httpClient.get<any>(this.url + 'Invoice/InvoiceById/' + id)
  }

  public addInvoiceToCart(cart: any): Observable<APIResponse<boolean>> {
    return this.httpClient.post<any>(this.url + 'Baskets/AddBasketItem', cart)
  }

  public getCartProducts() {
    return this.httpClient.get<any>(this.url + 'Baskets/GetActiveBasket')
  }
}
