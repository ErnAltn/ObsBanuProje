import { Component, OnInit } from '@angular/core';
import { LookUp } from 'app/core/models/lookUp';
import { LocalStorageService } from 'app/core/services/local-storage.service';
import { OzlukBilgileriService } from '../ozluk-bilgileri/services/ozlukBilgileri.service';
import { OgrenciAlinanDerslerDto } from './models/ogrenicAlinanDerslerModel';

@Component({
  selector: 'app-ogrenci-alinan-dersler',
  templateUrl: './ogrenci-alinan-dersler.component.html',
  styleUrls: ['./ogrenci-alinan-dersler.component.css']
})
export class OgrenciAlinanDerslerComponent implements OnInit {

  constructor(private ogrenciService: OzlukBilgileriService, private localStorageService: LocalStorageService) { }
  alinanDerslerList: OgrenciAlinanDerslerDto[];
  filteredAlinanDerslerList: OgrenciAlinanDerslerDto[];
  akademikYilList: LookUp[]
  ogrenciDonemi: string;
  ogrenciSinifi: string;
  baslangicYili: number
  bitisYili: number
  donem: string
  sinifNumarasi: number
  isChange =  false



  ngOnInit(): void {
    this.getOgrencialinanDersler();
    this.getAkademikYil()

  }

  getOgrencialinanDersler() {
    this.ogrenciService.getOgrenciAlinanDersler(this.localStorageService.getUserId()).subscribe(data => {
      this.alinanDerslerList = data
      this.ogrenciDonemi = this.alinanDerslerList[0].ogrenciDonemi;
      this.ogrenciSinifi = this.alinanDerslerList[0].ogrenciSinifi;
      if(!this.isChange){
        this.getAkademikYil()
      }
    });
  };




  getAkademikYil() {
    const yillar = this.ogrenciDonemi.match(/\d{4}/g);
    console.log(yillar)

    if (yillar && yillar.length === 2) {
      this.baslangicYili = parseInt(yillar[0], 10);
      this.bitisYili = parseInt(yillar[1], 10);
      console.log("çalışşsanaasss")
    }

    const numberMatch = this.ogrenciSinifi.match(/^\d+/);
    this.sinifNumarasi = numberMatch ? parseInt(numberMatch[0], 10) : null;

    

    const seasonMatch = this.ogrenciSinifi.match(/\b(Güz|Bahar)\b$/);
    this.donem = seasonMatch ? seasonMatch[0] : null;

    console.log(this.sinifNumarasi)
    if(this.sinifNumarasi == 1 && this.donem == 'Güz'){
    console.log('1')
      this.akademikYilList  = [{id: 1,label: this.ogrenciDonemi}]
    }
    else if (this.sinifNumarasi == 1 && this.donem == 'Bahar'){
      console.log('2')
      this.akademikYilList = [
        {id: 1,label: this.ogrenciDonemi},
        {id: 2,label: this.baslangicYili + '-' + this.bitisYili + 'Güz' + ' Dönemi'},]
    }
    else if ( this.sinifNumarasi == 2 &&  this.donem == 'Güz'){
      console.log('2')
      this.akademikYilList = [{id: 1,label: this.ogrenciDonemi},
        {id: 2,label: this.baslangicYili-1 + '-' + this.baslangicYili + ' Bahar' + ' Dönemi'},
        {id: 3,label: this.baslangicYili-1 + '-' + this.baslangicYili + ' Güz' + ' Dönemi'}]
    }
    else if ( this.sinifNumarasi == 2 &&  this.donem == 'Bahar'){
      console.log('3')
      this.akademikYilList = [
        {id: 1,label: this.ogrenciDonemi},
        {id: 2,label: this.baslangicYili + '-' + this.baslangicYili + 'Güz' + ' Dönemi'},
        {id: 3,label: this.baslangicYili-1 + '-' + this.baslangicYili + ' Bahar' + ' Dönemi'},
        {id: 4,label: this.baslangicYili-1 + '-' + this.baslangicYili + ' Güz' + ' Dönemi'}]
    }
    else if ( this.sinifNumarasi == 3 &&  this.donem == 'Güz'){
      console.log('3')
      this.akademikYilList = [{id: 1,label: this.ogrenciDonemi},
        {id: 2,label: this.baslangicYili-1 + '-' + this.baslangicYili + ' Bahar' + ' Dönemi'},
        {id: 3,label: this.baslangicYili-1 + '-' + this.baslangicYili + ' Güz' + ' Dönemi'},
        {id: 4,label: this.baslangicYili-2 + '-' + (this.baslangicYili-1) + ' Bahar' + ' Dönemi'},
        {id: 5,label: this.baslangicYili-2 + '-' + (this.baslangicYili-1) + ' Güz' + ' Dönemi'}]
    }
    else if ( this.sinifNumarasi == 3 &&  this.donem == 'Bahar'){
      console.log('3')
      this.akademikYilList = [
        {id: 1,label: this.ogrenciDonemi},
        {id: 2,label: this.baslangicYili+ '-'+ this.bitisYili + ' Güz' + ' Dönemi'},
        {id: 3,label: this.baslangicYili-1 + '-' + this.baslangicYili + ' Bahar' + ' Dönemi'},
        {id: 4,label: this.baslangicYili-1 + '-' + this.baslangicYili + ' Güz' + ' Dönemi'},
        {id: 5,label: this.baslangicYili-2 + '-' + (this.baslangicYili-1) + ' Bahar' + ' Dönemi'},
        {id: 6,label: this.baslangicYili-2 + '-' + (this.baslangicYili-1) + ' Güz' + ' Dönemi'}]
    }
    else if ( this.sinifNumarasi == 4 &&  this.donem == 'Güz'){
      console.log('4')
      this.akademikYilList = [{id: 1,label: this.ogrenciDonemi},
        {id: 2,label: this.baslangicYili-1 + '-' + this.baslangicYili + ' Bahar' + ' Dönemi'},
        {id: 3,label: this.baslangicYili-1 + '-' + this.baslangicYili + ' Güz' + ' Dönemi'},
        {id: 4,label: this.baslangicYili-2 + '-' + (this.baslangicYili-1) + ' Bahar' + ' Dönemi'},
        {id: 5,label: this.baslangicYili-2 + '-' + (this.baslangicYili-1) + ' Güz' + ' Dönemi'},
        {id: 6,label: this.baslangicYili-3 + '-' + (this.baslangicYili-2) + ' Bahar' + ' Dönemi'},
        {id: 7,label: this.baslangicYili-3 + '-' + (this.baslangicYili-2) + ' Güz' + ' Dönemi'}]
    }
    else if ( this.sinifNumarasi == 4 &&  this.donem == 'Bahar'){
      console.log('4')
      this.akademikYilList = [
        {id: 1,label: this.ogrenciDonemi},
        {id: 2,label: this.baslangicYili+ '-'+ this.bitisYili + ' Güz' + ' Dönemi'},
        {id: 3,label: this.baslangicYili-1 + '-' + this.baslangicYili + ' Bahar' + ' Dönemi'},
        {id: 4,label: this.baslangicYili-1 + '-' + this.baslangicYili + ' Güz' + ' Dönemi'},
        {id: 5,label: this.baslangicYili-2 + '-' + (this.baslangicYili-1) + ' Bahar' + ' Dönemi'},
        {id: 6,label: this.baslangicYili-2 + '-' + (this.baslangicYili-1) + ' Güz' + ' Dönemi'},
        {id: 7,label: this.baslangicYili-3 + '-' + (this.baslangicYili-2) + ' Bahar' + ' Dönemi'},
        {id: 8,label: this.baslangicYili-3 + '-' + (this.baslangicYili-2) + ' Güz' + ' Dönemi'}]
    }
    console.log(this.akademikYilList)
  }

  filterByAkademikYil(akademikYil){
    if(this.isChange==false){
      this.filteredAlinanDerslerList= this.alinanDerslerList.filter(a=>a.dersVerilenDonem===akademikYil)
      this.isChange=true
    }
    else
    {
      this.getOgrencialinanDersler();
      this.filteredAlinanDerslerList= this.alinanDerslerList.filter(a=>a.dersVerilenDonem===akademikYil)
    }
  }
}
