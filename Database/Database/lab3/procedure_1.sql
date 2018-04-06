USE Belay;

-- Обновление цены на страховки, где
-- @coefficient REAL -- к-т на который умножаются цены
-- @sphere VARCHAR(100) - сфера для которой происходит обновление
-- обновление цен, которые находятся в диапаоне (@begin_with ; @end_with)
CREATE PROCEDURE UpdateInsurePrice (@coefficient REAL, @sphere VARCHAR(100), @begin_with MONEY, @end_with MONEY)
AS
BEGIN
  UPDATE Insure
  SET price = price*@coefficient
  WHERE sphere = @sphere AND price > @begin_with AND price < @end_with;
RETURN
END
;

EXECUTE UpdateInsurePrice @coefficient = 2, @sphere = 'Страхование от несчастных случаев',
                          @begin_with = 0, @end_with = 10000;

DROP PROCEDURE ShowTreeQty;

CREATE PROCEDURE ShowTreeQty
AS
BEGIN
    WITH list(id, gty, level) AS
    (SELECT id, qty_insure, 0 as level
     FROM Hierarchy
     WHERE Hierarchy.id_junior IS NULL
     UNION ALL
        SELECT h.id,(l.gty+h.qty_insure), level+1
        FROM list l, Hierarchy h
        WHERE l.id = h.id_junior)
    SELECT * FROM list
  ORDER BY level
END;

EXECUTE ShowTreeQty;