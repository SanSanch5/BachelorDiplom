CREATE OR REPLACE FUNCTION f_insert_crash_info(IN point, in timestamp without time zone, in int, in int[])
	AS
$BODY$
DO
$do$
declare new_crash integer;
declare m integer;
begin
	insert into t_crash(place, until) values ('(55.456163,65.323229)', '2015-05-13 22:34:01');
	select into new_crash id from t_crash where place ~= '(55.456163,65.323229)' and until = '2015-05-13 22:34:01';

	update t_transit set crash_id = new_crash where id = 18;

	foreach m in array array[1,4,10]
	loop
	  update mchs.staff set crash_id = new_crash where id = m;
	end loop;
end
$do$
$BODY$
  LANGUAGE sql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION f_insert_crash_info(point, timestamp without time zone, int, int[])
  OWNER TO postgres;
