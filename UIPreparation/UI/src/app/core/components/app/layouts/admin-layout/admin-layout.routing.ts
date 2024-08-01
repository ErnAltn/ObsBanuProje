import { Routes } from '@angular/router';
import { BolumComponent } from 'app/core/components/admin/bolum/bolum.component';
import { DanismanlikComponent } from 'app/core/components/admin/danismanlik/danismanlik.component';
import { DegerlendirmeComponent } from 'app/core/components/admin/degerlendirme/degerlendirme.component';
import { DersAcmaComponent } from 'app/core/components/admin/dersAcma/dersAcma.component';
import { DersAlmaComponent } from 'app/core/components/admin/dersAlma/dersAlma.component';
import { DersHavuzuComponent } from 'app/core/components/admin/dersHavuzu/dersHavuzu.component';
import { DerslikComponent } from 'app/core/components/admin/derslik/derslik.component';
import { DersProgramiComponent } from 'app/core/components/admin/dersProgrami/dersProgrami.component';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { LanguageComponent } from 'app/core/components/admin/language/language.component';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { MufredatComponent } from 'app/core/components/admin/mufredat/mufredat.component';
import { OgrenciComponent } from 'app/core/components/admin/ogrenci/ogrenci.component';
import { OgretimElemaniComponent } from 'app/core/components/admin/ogretimElemani/ogretimElemani.component';
import { OperationClaimComponent } from 'app/core/components/admin/operationclaim/operationClaim.component';
import { SinavComponent } from 'app/core/components/admin/sinav/sinav.component';
import { ST_AkademikDonemComponent } from 'app/core/components/admin/sT_AkademikDonem/sT_AkademikDonem.component';
import { ST_AkademikYilComponent } from 'app/core/components/admin/sT_AkademikYil/sT_AkademikYil.component';
import { ST_DersAlmaDurumuComponent } from 'app/core/components/admin/sT_DersAlmaDurumu/sT_DersAlmaDurumu.component';
import { ST_DersDiliComponent } from 'app/core/components/admin/sT_DersDili/sT_DersDili.component';
import { ST_DersGunuComponent } from 'app/core/components/admin/sT_DersGunu/sT_DersGunu.component';
import { ST_DerslikTuruComponent } from 'app/core/components/admin/sT_DerslikTuru/sT_DerslikTuru.component';
import { ST_DersSeviyesiComponent } from 'app/core/components/admin/sT_DersSeviyesi/sT_DersSeviyesi.component';
import { ST_DersTuruComponent } from 'app/core/components/admin/sT_DersTuru/sT_DersTuru.component';
import { ST_OgrenciDurumComponent } from 'app/core/components/admin/sT_OgrenciDurum/sT_OgrenciDurum.component';
import { ST_OgretimDiliComponent } from 'app/core/components/admin/sT_OgretimDili/sT_OgretimDili.component';
import { ST_OgretimTuruComponent } from 'app/core/components/admin/sT_OgretimTuru/sT_OgretimTuru.component';
import { ST_ProgramTuruComponent } from 'app/core/components/admin/sT_ProgramTuru/sT_ProgramTuru.component';
import { ST_SinavTuruComponent } from 'app/core/components/admin/sT_SinavTuru/sT_SinavTuru.component';
import { TranslateComponent } from 'app/core/components/admin/translate/translate.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { OgrenciAlinanDerslerComponent } from 'app/core/components/ogrenci/ogrenci-alinan-dersler/ogrenci-alinan-dersler.component';
import { OgrenciDersProgramiComponent } from 'app/core/components/ogrenci/ogrenci-ders-programi/ogrenci-ders-programi.component';
import { OgrenciMufredatComponent } from 'app/core/components/ogrenci/ogrenci-mufredat/ogrenci-mufredat.component';
import { OzlukBilgileriComponent } from 'app/core/components/ogrenci/ozluk-bilgileri/ozluk-bilgileri.component';
import { OgretimElemaniMufredatComponent } from 'app/core/components/ogretimElemani/ogretim-elemani-mufredat/ogretim-elemani-mufredat.component';
import { OgretimElemaniOzlukBilgileriComponent } from 'app/core/components/ogretimElemani/ogretim-elemani-ozluk-bilgileri/ogretim-elemani-ozluk-bilgileri.component';
import { SinavNotlandirmaComponent } from 'app/core/components/ogretimElemani/sinav-notlandirma/sinav-notlandirma.component';
import { LoginGuard } from 'app/core/guards/login-guard';
import { DashboardComponent } from '../../dashboard/dashboard.component';





export const AdminLayoutRoutes: Routes = [

    { path: 'dashboard',      component: DashboardComponent,canActivate:[LoginGuard]}, 
    { path: 'user',           component: UserComponent, canActivate:[LoginGuard] },
    { path: 'group',          component: GroupComponent, canActivate:[LoginGuard] },
    { path: 'login',          component: LoginComponent },
    { path: 'language',       component: LanguageComponent,canActivate:[LoginGuard]},
    { path: 'translate',      component: TranslateComponent,canActivate:[LoginGuard]},
    { path: 'operationclaim', component: OperationClaimComponent,canActivate:[LoginGuard]},
    { path: 'bolum',            component: BolumComponent,canActivate:[LoginGuard]},
    { path: 'danismanlik',            component: DanismanlikComponent,canActivate:[LoginGuard]},
    { path: 'degerlendirme',            component: DegerlendirmeComponent,canActivate:[LoginGuard]},
    { path: 'dersAcma',            component: DersAcmaComponent,canActivate:[LoginGuard]},
    { path: 'dersAlma',            component: DersAlmaComponent,canActivate:[LoginGuard]},
    { path: 'dersHavuzu',            component: DersHavuzuComponent,canActivate:[LoginGuard]},
    { path: 'derslik',            component: DerslikComponent,canActivate:[LoginGuard]},
    { path: 'dersProgrami',            component: DersProgramiComponent,canActivate:[LoginGuard]},
    { path: 'mufredat',            component: MufredatComponent,canActivate:[LoginGuard]},
    { path: 'ogrenci',            component: OgrenciComponent,canActivate:[LoginGuard]},
    { path: 'ogretimElemani',            component: OgretimElemaniComponent,canActivate:[LoginGuard]},
    { path: 'sinav',            component: SinavComponent,canActivate:[LoginGuard]},
    { path: 'sT_AkademikDonem',            component: ST_AkademikDonemComponent,canActivate:[LoginGuard]},
    { path: 'sT_AkademikYil',            component: ST_AkademikYilComponent,canActivate:[LoginGuard]},
    { path: 'sT_DersAlmaDurumu',            component: ST_DersAlmaDurumuComponent,canActivate:[LoginGuard]},
    { path: 'sT_DersDili',            component: ST_DersDiliComponent,canActivate:[LoginGuard]},
    { path: 'sT_DersGunu',            component: ST_DersGunuComponent,canActivate:[LoginGuard]},
    { path: 'sT_DerslikTuru',            component: ST_DerslikTuruComponent,canActivate:[LoginGuard]},
    { path: 'sT_DersSeviyesi',            component: ST_DersSeviyesiComponent,canActivate:[LoginGuard]},
    { path: 'sT_DersTuru',            component: ST_DersTuruComponent,canActivate:[LoginGuard]},
    { path: 'sT_OgrenciDurum',            component: ST_OgrenciDurumComponent,canActivate:[LoginGuard]},
    { path: 'sT_OgretimDili',            component: ST_OgretimDiliComponent,canActivate:[LoginGuard]},
    { path: 'sT_OgretimTuru',            component: ST_OgretimTuruComponent,canActivate:[LoginGuard]},
    { path: 'sT_ProgramTuru',            component: ST_ProgramTuruComponent,canActivate:[LoginGuard]},
    { path: 'sT_SinavTuru',            component: ST_SinavTuruComponent,canActivate:[LoginGuard]},
    { path: 'ozluk-bilgileri',            component: OzlukBilgileriComponent,canActivate:[LoginGuard]},
    { path: 'ogrenci-mufredat',            component: OgrenciMufredatComponent,canActivate:[LoginGuard]},
    { path: 'ogretim-elemani-ozluk-bilgileri',            component: OgretimElemaniOzlukBilgileriComponent,canActivate:[LoginGuard]},
    { path: 'sinav-notlandirma',            component: SinavNotlandirmaComponent,canActivate:[LoginGuard]},
    { path: 'ogretim-elemani-mufredat',            component: OgretimElemaniMufredatComponent,canActivate:[LoginGuard]},
    { path: 'ogrenci-alinan-dersler',            component: OgrenciAlinanDerslerComponent,canActivate:[LoginGuard]},
    { path: 'ogrenci-ders-programi',            component: OgrenciDersProgramiComponent,canActivate:[LoginGuard]},
];
