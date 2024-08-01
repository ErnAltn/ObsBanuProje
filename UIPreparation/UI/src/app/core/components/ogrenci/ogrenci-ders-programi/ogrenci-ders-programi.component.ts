import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { LookUp } from 'app/core/models/lookUp';
import { LocalStorageService } from 'app/core/services/local-storage.service';
import { OzlukBilgileriService } from '../ozluk-bilgileri/services/ozlukBilgileri.service';
import { DersProgramiDto } from './models/dersProgramiDto';

@Component({
  selector: 'app-ogrenci-ders-programi',
  templateUrl: './ogrenci-ders-programi.component.html',
  styleUrls: ['./ogrenci-ders-programi.component.css']
})
export class OgrenciDersProgramiComponent implements OnInit, AfterViewInit {
  @ViewChild('tableContainer', { static: true  }) tableContainer: ElementRef;
  private cdRef: ChangeDetectorRef
  dersler: DersProgramiDto[];
  filteredDersProgrami: DersProgramiDto[];
  akademikYilList: LookUp[]
  ogrenciDonemi: string;
  ogrenciSinifi: string;
  baslangicYili: number
  bitisYili: number
  donem: string
  sinifNumarasi: number
  bolumAdi: string;
  saatler: any[] = [
    { id: 1, label: "08:45-09:30" },
    { id: 2, label: "09:35-10:20" },
    { id: 3, label: "10:25-11:10" },
    { id: 4, label: "11:15-12:00" },
    { id: 5, label: "12:00-12:50" },
    { id: 6, label: "12:50-13:35" },
    { id: 7, label: "13:40-14:25" },
    { id: 8, label: "14:30-15:15" },
    { id: 9, label: "15:20-16:05" },
    { id: 10, label: "16:10-16:55" },
    { id: 11, label: "17:00-17:45" }
  ];
  isChange = false;
  constructor(private ogrenciService: OzlukBilgileriService, private localStorageService: LocalStorageService) { }

  ngOnInit(): void {
    this.getDersProgramiList();
    this.getAkademikYil();
  }

  ngAfterViewInit() {
    if (this.tableContainer && this.tableContainer.nativeElement) {
      let tableContent = this.tableContainer.nativeElement.innerHTML;
      console.log(tableContent);
    } else {
      console.error('tableContainer elementi tanımlı değil veya DOM\'a eklenmedi.');
    }
  }


  getDersProgramiList() {
    this.ogrenciService.getOgrenciDersProgrami(this.localStorageService.getUserId()).subscribe(data => {
      this.dersler = data
      this.ogrenciDonemi = this.dersler[0].ogrenciDonemi;
      this.ogrenciSinifi = this.dersler[0].ogrenciSinifi;
      this.bolumAdi = this.dersler[0].bolumAdi;

      this.filteredDersProgrami = data.filter(d => d.ogrenciDonemi == d.dersVerilenDonem)
      console.log(this.filteredDersProgrami)
      if (!this.isChange) {
        this.getAkademikYil()
      }
    })
  }

  isSaatBlack(saatLabel: string): boolean {
    if (saatLabel == "12:00-12:50") {
      return true;
    }
  }

  getAkademikYil() {


    const yillar = this.ogrenciDonemi.match(/\d{4}/g);

    if (yillar && yillar.length === 2) {
      this.baslangicYili = parseInt(yillar[0], 10);
      this.bitisYili = parseInt(yillar[1], 10);
    }

    const numberMatch = this.ogrenciSinifi.match(/^\d+/);
    this.sinifNumarasi = numberMatch ? parseInt(numberMatch[0], 10) : null;



    const seasonMatch = this.ogrenciSinifi.match(/\b(Güz|Bahar)\b$/);
    this.donem = seasonMatch ? seasonMatch[0] : null;

    console.log(this.sinifNumarasi)
    if (this.sinifNumarasi == 1 && this.donem == 'Güz') {

      this.akademikYilList = [{ id: 1, label: this.ogrenciDonemi }]
    }
    else if (this.sinifNumarasi == 1 && this.donem == 'Bahar') {

      this.akademikYilList = [
        { id: 1, label: this.ogrenciDonemi },
        { id: 2, label: this.baslangicYili + '-' + this.bitisYili + 'Güz' + ' Dönemi' },]
    }
    else if (this.sinifNumarasi == 2 && this.donem == 'Güz') {

      this.akademikYilList = [{ id: 1, label: this.ogrenciDonemi },
      { id: 2, label: this.baslangicYili - 1 + '-' + this.baslangicYili + ' Bahar' + ' Dönemi' },
      { id: 3, label: this.baslangicYili - 1 + '-' + this.baslangicYili + ' Güz' + ' Dönemi' }]
    }
    else if (this.sinifNumarasi == 2 && this.donem == 'Bahar') {

      this.akademikYilList = [
        { id: 1, label: this.ogrenciDonemi },
        { id: 2, label: this.baslangicYili + '-' + this.baslangicYili + 'Güz' + ' Dönemi' },
        { id: 3, label: this.baslangicYili - 1 + '-' + this.baslangicYili + ' Bahar' + ' Dönemi' },
        { id: 4, label: this.baslangicYili - 1 + '-' + this.baslangicYili + ' Güz' + ' Dönemi' }]
    }
    else if (this.sinifNumarasi == 3 && this.donem == 'Güz') {

      this.akademikYilList = [{ id: 1, label: this.ogrenciDonemi },
      { id: 2, label: this.baslangicYili - 1 + '-' + this.baslangicYili + ' Bahar' + ' Dönemi' },
      { id: 3, label: this.baslangicYili - 1 + '-' + this.baslangicYili + ' Güz' + ' Dönemi' },
      { id: 4, label: this.baslangicYili - 2 + '-' + (this.baslangicYili - 1) + ' Bahar' + ' Dönemi' },
      { id: 5, label: this.baslangicYili - 2 + '-' + (this.baslangicYili - 1) + ' Güz' + ' Dönemi' }]
    }
    else if (this.sinifNumarasi == 3 && this.donem == 'Bahar') {

      this.akademikYilList = [
        { id: 1, label: this.ogrenciDonemi },
        { id: 2, label: this.baslangicYili + '-' + this.bitisYili + ' Güz' + ' Dönemi' },
        { id: 3, label: this.baslangicYili - 1 + '-' + this.baslangicYili + ' Bahar' + ' Dönemi' },
        { id: 4, label: this.baslangicYili - 1 + '-' + this.baslangicYili + ' Güz' + ' Dönemi' },
        { id: 5, label: this.baslangicYili - 2 + '-' + (this.baslangicYili - 1) + ' Bahar' + ' Dönemi' },
        { id: 6, label: this.baslangicYili - 2 + '-' + (this.baslangicYili - 1) + ' Güz' + ' Dönemi' }]
    }
    else if (this.sinifNumarasi == 4 && this.donem == 'Güz') {

      this.akademikYilList = [{ id: 1, label: this.ogrenciDonemi },
      { id: 2, label: this.baslangicYili - 1 + '-' + this.baslangicYili + ' Bahar' + ' Dönemi' },
      { id: 3, label: this.baslangicYili - 1 + '-' + this.baslangicYili + ' Güz' + ' Dönemi' },
      { id: 4, label: this.baslangicYili - 2 + '-' + (this.baslangicYili - 1) + ' Bahar' + ' Dönemi' },
      { id: 5, label: this.baslangicYili - 2 + '-' + (this.baslangicYili - 1) + ' Güz' + ' Dönemi' },
      { id: 6, label: this.baslangicYili - 3 + '-' + (this.baslangicYili - 2) + ' Bahar' + ' Dönemi' },
      { id: 7, label: this.baslangicYili - 3 + '-' + (this.baslangicYili - 2) + ' Güz' + ' Dönemi' }]
    }
    else if (this.sinifNumarasi == 4 && this.donem == 'Bahar') {

      this.akademikYilList = [
        { id: 1, label: this.ogrenciDonemi },
        { id: 2, label: this.baslangicYili + '-' + this.bitisYili + ' Güz' + ' Dönemi' },
        { id: 3, label: this.baslangicYili - 1 + '-' + this.baslangicYili + ' Bahar' + ' Dönemi' },
        { id: 4, label: this.baslangicYili - 1 + '-' + this.baslangicYili + ' Güz' + ' Dönemi' },
        { id: 5, label: this.baslangicYili - 2 + '-' + (this.baslangicYili - 1) + ' Bahar' + ' Dönemi' },
        { id: 6, label: this.baslangicYili - 2 + '-' + (this.baslangicYili - 1) + ' Güz' + ' Dönemi' },
        { id: 7, label: this.baslangicYili - 3 + '-' + (this.baslangicYili - 2) + ' Bahar' + ' Dönemi' },
        { id: 8, label: this.baslangicYili - 3 + '-' + (this.baslangicYili - 2) + ' Güz' + ' Dönemi' }]
    }

  }

  filterByAkademikYil(akademikYil: string) {

    if (this.isChange == false) {
      this.filteredDersProgrami = this.dersler.filter(a => a.dersVerilenDonem === akademikYil)
      this.bolumAdi = this.filteredDersProgrami[0].bolumAdi;
      this.ogrenciDonemi = this.filteredDersProgrami[0].ogrenciDonemi;
      this.ogrenciSinifi = this.filteredDersProgrami[0].ogrenciSinifi;
     
      if (this.tableContainer && this.tableContainer.nativeElement) {
    
         let tableContent = this.tableContainer.nativeElement.innerHTML;
        console.log(tableContent);
      } else {
        console.error('tableContainer elementi tanımlı değil veya DOM\'a eklenmedi.');
      }

      this.isChange = true
    }
    else {
      this.filteredDersProgrami = this.dersler.filter(a => a.dersVerilenDonem === akademikYil)
      this.bolumAdi = this.filteredDersProgrami[0].bolumAdi;
      this.ogrenciDonemi = this.filteredDersProgrami[0].dersVerilenDonem;
      this.ogrenciSinifi = this.filteredDersProgrami[0].ogrenciSinifi;
      if (this.tableContainer && this.tableContainer.nativeElement) {
  
        let tableContent = this.tableContainer.nativeElement.innerHTML;
        console.log(tableContent);
      } else {
        console.error('tableContainer elementi tanımlı değil veya DOM\'a eklenmedi.');
      }

    }

  }

  getDersForGunAndSaat(gun: string, saat: string): string {
    const filteredDers = this.filteredDersProgrami.find(
      d => d.dersSaati === saat && d.dersGunu === gun
    );
    if (gun == "Çarşamba" && saat == "12:00-12:50") {
      return 'Öğle Arası'
    }
    if (filteredDers) {
      return `${filteredDers.dersKodu} 
              <span style="font-weight: bold;">${filteredDers.dersAdi}</span>
              ${filteredDers.ogretimElemaniBilgisi}\n${filteredDers.derslikAdi}`;
    } else {
      return '';
    }

  }




}
