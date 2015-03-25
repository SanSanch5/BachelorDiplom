use Course_DB
go

select R1.TransID
from Routes join Routes as R1
	on Routes.TransID = R1.TransID
where Routes.StartTime <> R1.StartTime
