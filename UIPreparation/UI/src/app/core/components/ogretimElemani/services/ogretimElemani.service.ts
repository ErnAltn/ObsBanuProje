import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { OgretimElemaniMufredatDto } from '../ogretim-elemani-mufredat/models/mufredatDto';
import { OgretimElemaniOzlukBilgileriDto } from '../ogretim-elemani-ozluk-bilgileri/models/ogretimElemaniOzlukBilgileriDto';
import { OgretimElemaniSinavDto } from '../sinav-notlandirma/models/ogretimElemaniSinavlarDto';
import { DegerlendirmeDto } from '../sinav-notlandirma/models/sinavOgrenciDto';


@Injectable({
  providedIn: 'root'
})
export class OgretimElemaniServiceIn {

  constructor(private httpClient: HttpClient) { }

  getOgretimElemaniOzlukBilgileri(id:number): Observable<OgretimElemaniOzlukBilgileriDto> {
    return this.httpClient.get<OgretimElemaniOzlukBilgileriDto>(environment.getApiUrl + '/OgretimElemanis/getOgretimElemaniOzlukBilgileri?id='+id)
  }

  getOgretimElemaniSinav(id:number): Observable<OgretimElemaniSinavDto[]> {
    return this.httpClient.get<OgretimElemaniSinavDto[]>(environment.getApiUrl + '/OgretimElemanis/getOgretimElemaniSinavlar?id='+id)
  }


  getSinavOgrencileri(id:number): Observable<DegerlendirmeDto[]> {
    return this.httpClient.get<DegerlendirmeDto[]>(environment.getApiUrl + '/OgretimElemanis/getOgrenciler?id='+id)
  }

  getOgretimElemaniMufredat(id:number): Observable<OgretimElemaniMufredatDto[]> {
    return this.httpClient.get<OgretimElemaniMufredatDto[]>(environment.getApiUrl + '/OgretimElemanis/getOgretimElemaniMufredat?id='+id)
  }


  
}