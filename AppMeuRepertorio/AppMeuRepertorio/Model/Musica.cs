

using SQLite;

namespace AppMeuRepertorio.Model
{

    public class Musica
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Tom { get; set; }
        public string Letra { get; set; }
    }
}
