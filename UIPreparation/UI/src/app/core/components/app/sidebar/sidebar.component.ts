import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../admin/login/services/auth.service';


declare const $: any;
declare interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
  claim: string;
}
export const ADMINROUTES: RouteInfo[] = [
  { path: '/user', title: 'Users', icon: 'how_to_reg', class: '', claim: "GetUsersQuery" },
  { path: '/group', title: 'Groups', icon: 'groups', class: '', claim: "GetGroupsQuery" },
  { path: '/operationclaim', title: 'OperationClaim', icon: 'local_police', class: '', claim: "GetOperationClaimsQuery" },
  { path: '/language', title: 'Languages', icon: 'language', class: '', claim: "GetLanguagesQuery" },
  { path: '/translate', title: 'TranslateWords', icon: 'translate', class: '', claim: "GetTranslatesQuery" },
  { path: '/log', title: 'İşlem Kayıtları', icon: 'update', class: '', claim: "GetLogDtoQuery" },
  { path: '/bolum', title: 'Bölümler', icon: 'update', class: '', claim: "GetBolumsQuery" },
  { path: '/danismanlik', title: 'Danışmanlıkla', icon: 'update', class: '', claim: "GetDanismanliksQuery" },
  { path: '/degerlendirme', title: 'Değerlendirmeler', icon: 'update', class: '', claim: "GetDegerlendirmesQuery" },
  { path: '/dersAcma', title: 'Ders Açma İşlemleri', icon: 'update', class: '', claim: "GetDersAcmasQuery" },
  { path: '/dersAlma', title: 'Ders Alma İşlemleri', icon: 'update', class: '', claim: "GetDersAlmasQuery" },
  { path: '/dersHavuzu', title: 'Ders Havuzu', icon: 'update', class: '', claim: "GetDersHavuzusQuery" },
  { path: '/derslik', title: 'Derslikler', icon: 'update', class: '', claim: "GetDersliksQuery" },
  { path: '/dersProgrami', title: 'Ders Programı', icon: 'update', class: '', claim: "GetLogDtoQuery" },
  { path: '/mufredat', title: 'Müfredat', icon: 'update', class: '', claim: "GetMufredatsQuery" },
  { path: '/ogrenci', title: 'Öğrenciler', icon: 'update', class: '', claim: "GetOgrencisQuery" },
  { path: '/ogretimElemani', title: 'Öğretim Elemanları', icon: 'update', class: '', claim: "GetOgretimElemanisQuery" },
  { path: '/sinav', title: 'Sınav Tanımla', icon: 'update', class: '', claim: "GetSinavsQuery" }
];

export const USERROUTES: RouteInfo[] = [
  { path: '/sT_AkademikDonem', title: 'Akademik Dönem', icon: 'update', class: '', claim: "GetST_AkademikDonemsQuery" },
  { path: '/sT_AkademikYil', title: 'Akademik Yıl', icon: 'update', class: '', claim: "GetST_AkademikYilsQuery" },
  { path: '/sT_DersAlmaDurumu', title: 'Ders Alma Durumu', icon: 'update', class: '', claim: "GetST_DersAlmaDurumusQuery" },
  { path: '/sT_DersDili', title: 'Ders Dilleri', icon: 'update', class: '', claim: "GetST_DersDilisQuery" },
  { path: '/sT_DersGunu', title: 'Ders Günleri', icon: 'update', class: '', claim: "GetST_DersGunusQuery" },
  { path: '/sT_DerslikTuru', title: 'Derslik Türleri', icon: 'update', class: '', claim: "GetST_DerslikTurusQuery" },
  { path: '/sT_DersSeviyesi', title: 'Ders Seviyeleri', icon: 'update', class: '', claim: "GetST_DersSeviyesisQuery" },
  { path: '/sT_DersTuru', title: 'Ders Türleri', icon: 'update', class: '', claim: "GetST_DersTurusQuery" },
  { path: '/sT_OgrenciDurum', title: 'Öğrenci Durumları', icon: 'update', class: '', claim: "GetST_OgrenciDurumsQuery" },
  { path: '/sT_OgretimDili', title: 'Öğretim Dilleri', icon: 'update', class: '', claim: "GetST_OgretimDilisQuery" },
  { path: '/sT_OgretimTuru', title: 'Öğretim Türleri', icon: 'update', class: '', claim: "GetST_OgretimTurusQuery" },
  { path: '/sT_ProgramTuru', title: 'Program Türleri', icon: 'update', class: '', claim: "GetST_ProgramTurusQuery" },
  { path: '/sT_SinavTuru', title: 'Sınav Türleri', icon: 'update', class: '', claim: "GetST_SinavTurusQuery" },
  { path: '/#', title: '', icon: '', class: '', claim: "GetST_SinavTurusQuery" },
];

export const STUDENTROUTES: RouteInfo[] = [
  { path: '/ozluk-bilgileri', title: 'Özlük Bilgileri', icon: 'update', class: '', claim: "GetOgrenciOzlukBilgileriDto" },
  { path: '/ogrenci-mufredat', title: 'Müfredat', icon: 'update', class: '', claim: "GetOgrenciBolumMufredatQuery" },
  { path: '/ogrenci-alinan-dersler', title: 'Alınan Dersler', icon: 'update', class: '', claim: "GetOgrenciAlinanDerslerQuery" },
  { path: '/ogrenci-ders-programi', title: 'Ders Programı', icon: 'update', class: '', claim: "GetOgrenciDersProgramiQuery" },
]
export const TEACHINGSTAFFROUTES: RouteInfo[] = [
  { path: '/ogretim-elemani-ozluk-bilgileri', title: 'Özlük Bilgileri', icon: 'update', class: '', claim: "TeachingStaff" },
  { path: '/sinav-notlandirma', title: 'Sınav Notlandırma', icon: 'update', class: '', claim: "TeachingStaff" },
  { path: '/ogretim-elemani-mufredat', title: 'Müfredat', icon: 'update', class: '', claim: "TeachingStaff" },


]

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  adminMenuItems: any[];
  userMenuItems: any[];
  studentMenuItems: any[];
  teachingStaffMenuItems: any[];


  constructor(private router: Router, private authService: AuthService, public translateService: TranslateService) {

  }

  ngOnInit() {

    this.adminMenuItems = ADMINROUTES.filter(menuItem => menuItem);
    this.userMenuItems = USERROUTES.filter(menuItem => menuItem);
    this.studentMenuItems = STUDENTROUTES.filter(menuItem => menuItem);
    this.teachingStaffMenuItems = TEACHINGSTAFFROUTES.filter(menuItem => menuItem)
    var lang = localStorage.getItem('lang') || 'tr-TR'
    this.translateService.use(lang);
  }

  isMobileMenu() {
    if ($(window).width() > 600) {
      return false;
    }
    return true;
  };

  checkClaim(claim: string): boolean {
    return this.authService.claimGuard(claim)
  }
  ngOnDestroy() {
    if (!this.authService.loggedIn()) {
      this.authService.logOut();
      this.router.navigateByUrl("/login");
    }
  }
}

