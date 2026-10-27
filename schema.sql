-- ==============================================
--  ProjectHub – SQLite Schema Script
--  Database file: projects.db
--  Run this to recreate the Projects table manually.
--  NOTE: The app calls EnsureCreated() on startup
--  via EF Core, so this script is optional.
-- ==============================================

CREATE TABLE IF NOT EXISTS "Projects" (
    "Id"          INTEGER  NOT NULL CONSTRAINT "PK_Projects" PRIMARY KEY AUTOINCREMENT,
    "Name"        TEXT     NOT NULL,
    "Description" TEXT     NULL,
    "Client"      TEXT     NOT NULL,
    "Status"      TEXT     NOT NULL DEFAULT 'NotStarted',
    -- Status values: NotStarted | InProgress | OnHold | Completed | Cancelled
    "Priority"    TEXT     NOT NULL DEFAULT 'Medium',
    -- Priority values: Low | Medium | High | Critical
    "StartDate"   TEXT     NOT NULL,
    "DueDate"     TEXT     NULL,
    "Budget"      REAL     NULL,
    "TeamLead"    TEXT     NULL,
    "CreatedAt"   TEXT     NOT NULL DEFAULT (datetime('now')),
    "UpdatedAt"   TEXT     NOT NULL DEFAULT (datetime('now'))
);

-- -----------------------------------------------
--  Optional: seed sample projects
-- -----------------------------------------------
INSERT INTO "Projects" ("Name","Description","Client","Status","Priority","StartDate","DueDate","Budget","TeamLead","CreatedAt","UpdatedAt")
VALUES
    ('Website Redesign',     'Full redesign of the company website.',        'Acme Corp',     'InProgress',  'High',     '2026-09-01', '2026-11-30', 12000.00, 'Jane Smith',  datetime('now'), datetime('now')),
    ('Mobile App v2',        'New features for the mobile application.',     'Beta Ltd',      'NotStarted',  'Critical', '2026-11-01', '2027-02-28', 45000.00, 'Tom Nguyen',  datetime('now'), datetime('now')),
    ('Data Migration',       'Migrate legacy DB to new cloud platform.',     'Gamma Inc',     'Completed',   'Medium',   '2026-07-01', '2026-09-30', 8500.00,  'Sara Khan',   datetime('now'), datetime('now')),
    ('Brand Identity',       'Logo, colours, and brand guidelines refresh.', 'Delta Studios', 'OnHold',      'Low',      '2026-08-15', '2026-12-15', 3200.00,  NULL,          datetime('now'), datetime('now'));
