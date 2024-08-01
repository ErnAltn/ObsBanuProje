import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OgrenciOzlukBilgileriDto } from 'app/core/components/admin/ogrenci/models/ogrenciOzlukBilgileriDto';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { OgrenciAlinanDerslerDto } from '../../ogrenci-alinan-dersler/models/ogrenicAlinanDerslerModel';
import { DersProgramiDto } from '../../ogrenci-ders-programi/models/dersProgramiDto';
import { OgrenciMufredatDto } from '../../ogrenci-mufredat/models/ogrenciMufredat';


@Injectable({
  providedIn: 'root'
})
export class OzlukBilgileriService {

  constructor(private httpClient: HttpClient) { }

  getOgrenciOzlukBilgileri(id:number): Observable<OgrenciOzlukBilgileriDto> {
    return this.httpClient.get<OgrenciOzlukBilgileriDto>(environment.getApiUrl + '/Ogrencis/getOzlukBilgileri?id='+id)
  }


  getOgrenciMufredat(id:number):Observable<OgrenciMufredatDto[]> {
    return this.httpClient.get<OgrenciMufredatDto[]>(environment.getApiUrl + '/Mufredats/getOgrenciMufredat?id='+id)
  }


  getOgrenciAlinanDersler(id:number):Observable<OgrenciAlinanDerslerDto[]> {
    return this.httpClient.get<OgrenciAlinanDerslerDto[]>(environment.getApiUrl + '/Ogrencis/getOgrenciAlinanDerslerDto?id='+id)
  }
  getOgrenciDersProgrami(id:number):Observable<DersProgramiDto[]> {
    return this.httpClient.get<DersProgramiDto[]>(environment.getApiUrl + '/Ogrencis/getOgrenciDersProgramiDto?id='+id)
  }
}