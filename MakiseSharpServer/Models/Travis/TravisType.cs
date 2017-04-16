namespace MakiseSharpServer.Models.Travis
{
    /// <summary>
    /// Type of Travis service (travis-ci.org/com)
    /// </summary>
    public enum TravisType
    {
        /// <summary>
        /// travis-ci.com for private projects
        /// </summary>
        Com,

        /// <summary>
        /// travis-ci.org for open source projects
        /// </summary>
        Org
    }
}
