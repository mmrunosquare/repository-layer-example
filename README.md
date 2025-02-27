# repository-layer-example
An example of how a new layer in our proyect would make our lives better

```md
# 🚀 Why Use the Repository Pattern in a 3-Layer Architecture?

In an architecture with **API, Business, and Data layers**, if you do **not** use a **Repository Pattern**, you may encounter several problems:

---

## 🔥 1️⃣ Tight Coupling (Tightly Coupled Code)
If the **Business Layer** (`Business`) directly accesses `DbContext`, it becomes tightly coupled to the specific database implementation.

```csharp
public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public User GetUser(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }
}
```

⚠ **Problems:**
- If you decide to switch from **Entity Framework to Dapper**, you will have to modify the entire business layer.
- **Unit testing becomes difficult** because you need a real database.

---

## 🔥 2️⃣ Cannot Mock the Database (Difficult Testing)
If `Business` directly uses `DbContext`, writing **unit tests** is harder because you depend on a real database.

✅ **Solution with Repository Pattern:**
```csharp
public interface IUserRepository
{
    User GetUser(int id);
}
```
Now you can **mock** data using **Moq** without needing a database:

```csharp
var mockRepo = new Mock<IUserRepository>();
mockRepo.Setup(repo => repo.GetUser(1)).Returns(new User { Id = 1, Name = "Byron" });

var service = new UserService(mockRepo.Object);
var user = service.GetUser(1);

Assert.Equal("Byron", user.Name);
```
✅ **Now, unit testing is easy without a real database.**

---

## 🔥 3️⃣ Repetitive Code (DRY Violation)
If every service in your **Business Layer** (`Business`) writes its own queries, you end up with **duplicate code**.

**Without Repository Pattern:**
```csharp
public class UserService
{
    private readonly AppDbContext _context;
    public UserService(AppDbContext context) => _context = context;

    public User GetUser(int id) => _context.Users.FirstOrDefault(u => u.Id == id);
}

public class OrderService
{
    private readonly AppDbContext _context;
    public OrderService(AppDbContext context) => _context = context;

    public Order GetOrder(int id) => _context.Orders.FirstOrDefault(o => o.Id == id);
}
```
🔴 **Problem:** Every service writes queries separately, leading to **redundant code**.

✅ **Solution with Repository Pattern**:
```csharp
public interface IUserRepository
{
    User GetUser(int id);
}

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context) => _context = context;

    public User GetUser(int id) => _context.Users.FirstOrDefault(u => u.Id == id);
}
```
🔹 **Benefit:** Any service that needs `User` data simply calls the repository instead of writing its own queries.

---

## 🔥 4️⃣ Hard to Switch Database or ORM
If your business layer directly uses `DbContext`, switching from **SQL Server to MongoDB** or **Dapper** requires **rewriting the entire business layer**.

✅ **Solution with Repository Pattern**:
```csharp
public interface IUserRepository
{
    User GetUser(int id);
}
```

Now, you can create different **repository implementations** for different database technologies **without touching the Business Layer**.

- **For Entity Framework:**
```csharp
public class EfUserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public EfUserRepository(AppDbContext context) => _context = context;

    public User GetUser(int id) => _context.Users.FirstOrDefault(u => u.Id == id);
}
```

- **For Dapper:**
```csharp
public class DapperUserRepository : IUserRepository
{
    private readonly IDbConnection _db;
    public DapperUserRepository(IDbConnection db) => _db = db;

    public User GetUser(int id)
    {
        return _db.Query<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id }).FirstOrDefault();
    }
}
```

🔹 **Benefits:**  
- If you change your ORM, **you only replace the repository implementation**, not the entire business logic.

---

## 🚀 **Conclusion**
If you **do not** use the Repository Pattern, you may face:
❌ **Tightly coupled code** between `Business` and `Database`.  
❌ **Difficult unit testing** because of direct `DbContext` dependency.  
❌ **Duplicate query logic** across multiple services.  
❌ **Hard to switch ORM or database** without major rewrites.  

### ✅ **Advantages of Using Repository Pattern**
✔ **Separation of concerns** → Keeps business logic independent of the database.  
✔ **Easier unit testing** → You can mock repositories instead of using a real database.  
✔ **Less repetitive code** → Centralized database access logic.  
✔ **Flexibility** → Change the database or ORM **without modifying the Business Layer**.  

🚀 **Using the Repository Pattern improves maintainability, testability, and scalability!**  
