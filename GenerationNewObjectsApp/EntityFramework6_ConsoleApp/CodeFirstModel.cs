namespace EntityFramework6_ConsoleApp
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CodeFirstModel : DbContext
    {
        // Контекст настроен для использования строки подключения "CodeFirstModel" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "EntityFramework6_ConsoleApp.CodeFirstModel" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "CodeFirstModel" 
        // в файле конфигурации приложения.
        public CodeFirstModel()
            : base("name=CodeFirstModel")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<PersonData> PersonDatas { get; set; }
    }

}