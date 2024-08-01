import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { ST_DersAlmaDurumu } from './models/ST_DersAlmaDurumu';
import { ST_DersAlmaDurumuService } from './services/ST_DersAlmaDurumu.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-sT_DersAlmaDurumu',
	templateUrl: './sT_DersAlmaDurumu.component.html',
	styleUrls: ['./sT_DersAlmaDurumu.component.scss']
})
export class ST_DersAlmaDurumuComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ad','ekstra', 'update','delete'];

	sT_DersAlmaDurumuList:ST_DersAlmaDurumu[];
	sT_DersAlmaDurumu:ST_DersAlmaDurumu=new ST_DersAlmaDurumu();

	sT_DersAlmaDurumuAddForm: FormGroup;


	sT_DersAlmaDurumuId:number;

	constructor(private sT_DersAlmaDurumuService:ST_DersAlmaDurumuService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getST_DersAlmaDurumuList();
    }

	ngOnInit() {

		this.createST_DersAlmaDurumuAddForm();
	}


	getST_DersAlmaDurumuList() {
		this.sT_DersAlmaDurumuService.getST_DersAlmaDurumuList().subscribe(data => {
			this.sT_DersAlmaDurumuList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sT_DersAlmaDurumuAddForm.valid) {
			this.sT_DersAlmaDurumu = Object.assign({}, this.sT_DersAlmaDurumuAddForm.value)

			if (this.sT_DersAlmaDurumu.id == 0)
				this.addST_DersAlmaDurumu();
			else
				this.updateST_DersAlmaDurumu();
		}

	}

	addST_DersAlmaDurumu(){

		this.sT_DersAlmaDurumuService.addST_DersAlmaDurumu(this.sT_DersAlmaDurumu).subscribe(data => {
			this.getST_DersAlmaDurumuList();
			this.sT_DersAlmaDurumu = new ST_DersAlmaDurumu();
			jQuery('#st_dersalmadurumu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_DersAlmaDurumuAddForm);

		})

	}

	updateST_DersAlmaDurumu(){

		this.sT_DersAlmaDurumuService.updateST_DersAlmaDurumu(this.sT_DersAlmaDurumu).subscribe(data => {

			var index=this.sT_DersAlmaDurumuList.findIndex(x=>x.id==this.sT_DersAlmaDurumu.id);
			this.sT_DersAlmaDurumuList[index]=this.sT_DersAlmaDurumu;
			this.dataSource = new MatTableDataSource(this.sT_DersAlmaDurumuList);
            this.configDataTable();
			this.sT_DersAlmaDurumu = new ST_DersAlmaDurumu();
			jQuery('#st_dersalmadurumu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_DersAlmaDurumuAddForm);

		})

	}

	createST_DersAlmaDurumuAddForm() {
		this.sT_DersAlmaDurumuAddForm = this.formBuilder.group({		
			id : [0],
ad : ["", Validators.required],
ekstra : ["", Validators.required]
		})
	}

	deleteST_DersAlmaDurumu(sT_DersAlmaDurumuId:number){
		this.sT_DersAlmaDurumuService.deleteST_DersAlmaDurumu(sT_DersAlmaDurumuId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sT_DersAlmaDurumuList=this.sT_DersAlmaDurumuList.filter(x=> x.id!=sT_DersAlmaDurumuId);
			this.dataSource = new MatTableDataSource(this.sT_DersAlmaDurumuList);
			this.configDataTable();
		})
	}

	getST_DersAlmaDurumuById(sT_DersAlmaDurumuId:number){
		this.clearFormGroup(this.sT_DersAlmaDurumuAddForm);
		this.sT_DersAlmaDurumuService.getST_DersAlmaDurumuById(sT_DersAlmaDurumuId).subscribe(data=>{
			this.sT_DersAlmaDurumu=data;
			this.sT_DersAlmaDurumuAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
