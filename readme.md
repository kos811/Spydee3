### сборка докер образа
 docker build -f Dockerfile -t kos811.spydee3:latest --no-cache .

### запуск в докере под 6000 портом:
docker run --publish 6000:80 kos811.spydee3 -d

### таблица
```sql
DROP TABLE IF EXISTS default.pages;
CREATE TABLE default.pages
(
page_uri        text,
page_version    Int64,
page_content    text,
page_created_at DateTime
)
ENGINE = MergeTree() PRIMARY KEY (page_uri, page_version)
ORDER BY (page_uri, page_version)
SETTINGS index_granularity = 8192;

INSERT INTO pages (page_uri, page_version, page_content, page_created_at)
VALUES ('https://ya.ru', 0, 'empty', now());
```
