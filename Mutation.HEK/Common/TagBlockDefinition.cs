﻿using Mutation.HEK.Common.TagFieldDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.HEK.Common
{
    public class TagBlockDefinition : ITagDefinition
    {
        /// <summary>
        /// Gets the tag_block_defintion struct this object encapsulates.
        /// </summary>
        public tag_block_definition s_tag_block_definition;// { get; set; }

        /// <summary>
        /// Gets the array of tag_field_set structs the tag_blocK_definition
        /// </summary>
        public tag_field_set[] TagFieldSets { get; set; }

        /// <summary>
        /// Gets a 2-dimensional array that contains sets of tag_field's for each tag_field_set in the tag_block_definition struct.
        /// </summary>
        public tag_field[][] TagFields { get; set; }

        /// <summary>
        /// Gets an index into the TagFieldSets array for the latest tag field set.
        /// </summary>
        public int TagFieldSetLatestIndex { get; set; }

        public TagBlockDefinition()
        {
            // Initialize the tag_block_definition.
            this.s_tag_block_definition = new tag_block_definition();
        }

        public void Read(IntPtr h2LangLib, System.IO.BinaryReader reader)
        {

        }
    }
}