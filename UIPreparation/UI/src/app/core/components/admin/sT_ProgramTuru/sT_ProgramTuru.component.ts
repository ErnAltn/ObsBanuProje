import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { ST_ProgramTuru } from './models/ST_ProgramTuru';
import { ST_ProgramTuruService } from './services/ST_ProgramTuru.service';

declare var jQuery: any;

@Component({
	selector: 'app-sT_ProgramTuru',
	templateUrl: './sT_ProgramTuru.component.html',
	styleUrls: ['./sT_ProgramTuru.component.scss']
})
export class ST_ProgramTuruComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ad','ekstra', 'update','delete'];

	sT_ProgramTuruList:ST_ProgramTuru[];
	sT_ProgramTuru:ST_ProgramTuru=new ST_ProgramTuru();

	sT_ProgramTuruAddForm: FormGroup;


	sT_ProgramTuruId:number;

	constructor(private sT_ProgramTuruService:ST_ProgramTuruService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getST_ProgramTuruList();
    }

	ngOnInit() {

		this.createST_ProgramTuruAddForm();
	}


	getST_ProgramTuruList() {
		this.sT_ProgramTuruService.getST_ProgramTuruList().subscribe(data => {
			this.sT_ProgramTuruList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sT_ProgramTuruAddForm.valid) {
			this.sT_ProgramTuru = Object.assign({}, this.sT_ProgramTuruAddForm.value)

			if (this.sT_ProgramTuru.id == 0)
				this.addST_ProgramTuru();
			else
				this.updateST_ProgramTuru();
		}

	}

	addST_ProgramTuru(){

		this.sT_ProgramTuruService.addST_ProgramTuru(this.sT_ProgramTuru).subscribe(data => {
			this.getST_ProgramTuruList();
			this.sT_ProgramTuru = new ST_ProgramTuru();
			jQuery('#st_programturu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_ProgramTuruAddForm);

		})

	}

	updateST_ProgramTuru(){

		this.sT_ProgramTuruService.updateST_ProgramTuru(this.sT_ProgramTuru).subscribe(data => {

			var index=this.sT_ProgramTuruList.findIndex(x=>x.id==this.sT_ProgramTuru.id);
			this.sT_ProgramTuruList[index]=this.sT_ProgramTuru;
			this.dataSource = new MatTableDataSource(this.sT_ProgramTuruList);
            this.configDataTable();
			this.sT_ProgramTuru = new ST_ProgramTuru();
			jQuery('#st_programturu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_ProgramTuruAddForm);

		})

	}

	createST_ProgramTuruAddForm() {
		this.sT_ProgramTuruAddForm = this.formBuilder.group({		
			id : [0],
ad : ["", Validators.required],
ekstra : [""]
		})
	}

	deleteST_ProgramTuru(sT_ProgramTuruId:number){
		this.sT_ProgramTuruService.deleteST_ProgramTuru(sT_ProgramTuruId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sT_ProgramTuruList=this.sT_ProgramTuruList.filter(x=> x.id!=sT_ProgramTuruId);
			this.dataSource = new MatTableDataSource(this.sT_ProgramTuruList);
			this.configDataTable();
		})
	}

	getST_ProgramTuruById(sT_ProgramTuruId:number){
		this.clearFormGroup(this.sT_ProgramTuruAddForm);
		this.sT_ProgramTuruService.getST_ProgramTuruById(sT_ProgramTuruId).subscribe(data=>{
			this.sT_ProgramTuru=data;
			this.sT_ProgramTuruAddForm.patchValue(data);
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
