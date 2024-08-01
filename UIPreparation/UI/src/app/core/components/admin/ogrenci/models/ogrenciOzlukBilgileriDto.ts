export class OgrenciOzlukBilgileriDto{
    adi?: string;
    soyadi?: string;
    tcKimlikNo?: string;
    dogumTarihi?: (Date|any);
    bolumAdi?: string;
    ogrNo?: string;
    kayitTarihi?: (Date|any);
    durum?: string;
    ayrilmaTarihi?: (Date|any);
    danismanAdi?: string;
    danismanSoyadi?: string;
    adres?: string;
    telefonNo?: string;
    constructor(
        adi?: string,
        soyadi?: string,
        tcKimlikNo?: string,
        dogumTarihi?: Date | any,
        bolumAdi?: string,
        ogrNo?: string,
        kayitTarihi?: Date | any,
        durum?: string,
        ayrilmaTarihi?: Date | any,
        danismanAdi?: string,
        danismanSoyadi?: string,
        adres?: string,
        telefonNo?: string
    ) {
        this.adi = adi;
        this.soyadi = soyadi;
        this.tcKimlikNo = tcKimlikNo;
        this.dogumTarihi = dogumTarihi;
        this.bolumAdi = bolumAdi;
        this.ogrNo = ogrNo;
        this.kayitTarihi = kayitTarihi;
        this.durum = durum;
        this.ayrilmaTarihi = ayrilmaTarihi;
        this.danismanAdi = danismanAdi;
        this.danismanSoyadi = danismanSoyadi;
        this.adres = adres;
        this.telefonNo = telefonNo;
    }
}