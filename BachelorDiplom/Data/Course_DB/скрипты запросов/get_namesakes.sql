use Course_DB
go

select D1.ID
from Drivers join Drivers as D1
	on Drivers.LName = D1.LName 
		and Drivers.MName = D1.MName
		and Drivers.Name = D1.Name
where Drivers.ID <> D1.ID
