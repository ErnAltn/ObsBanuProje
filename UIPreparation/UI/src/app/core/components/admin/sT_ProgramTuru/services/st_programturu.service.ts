import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { ST_ProgramTuru } from '../models/ST_ProgramTuru';


@Injectable({
  providedIn: 'root'
})
export class ST_ProgramTuruService {

  constructor(private httpClient: HttpClient) { }


  getST_ProgramTuruList(): Observable<ST_ProgramTuru[]> {

    return this.httpClient.get<ST_ProgramTuru[]>(environment.getApiUrl + '/ST_ProgramTurus/getall')
  }

  getST_ProgramTuruById(id: number): Observable<ST_ProgramTuru> {
    return this.httpClient.get<ST_ProgramTuru>(environment.getApiUrl + '/ST_ProgramTurus/getbyid?id='+id)
  }

  addST_ProgramTuru(sT_ProgramTuru: ST_ProgramTuru): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/ST_ProgramTurus/', sT_ProgramTuru, { responseType: 'text' });
  }

  updateST_ProgramTuru(sT_ProgramTuru: ST_ProgramTuru): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/ST_ProgramTurus/', sT_ProgramTuru, { responseType: 'text' });

  }

  deleteST_ProgramTuru(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/ST_ProgramTurus/', { body: { id: id } });
  }


}