import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { ST_DersAlmaDurumu } from '../models/ST_DersAlmaDurumu';


@Injectable({
  providedIn: 'root'
})
export class ST_DersAlmaDurumuService {

  constructor(private httpClient: HttpClient) { }


  getST_DersAlmaDurumuList(): Observable<ST_DersAlmaDurumu[]> {

    return this.httpClient.get<ST_DersAlmaDurumu[]>(environment.getApiUrl + '/ST_DersAlmaDurumus/getall')
  }

  getST_DersAlmaDurumuById(id: number): Observable<ST_DersAlmaDurumu> {
    return this.httpClient.get<ST_DersAlmaDurumu>(environment.getApiUrl + '/ST_DersAlmaDurumus/getbyid?id='+id)
  }

  addST_DersAlmaDurumu(sT_DersAlmaDurumu: ST_DersAlmaDurumu): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/ST_DersAlmaDurumus/', sT_DersAlmaDurumu, { responseType: 'text' });
  }

  updateST_DersAlmaDurumu(sT_DersAlmaDurumu: ST_DersAlmaDurumu): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/ST_DersAlmaDurumus/', sT_DersAlmaDurumu, { responseType: 'text' });

  }

  deleteST_DersAlmaDurumu(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/ST_DersAlmaDurumus/', { body: { id: id } });
  }


}