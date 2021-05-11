﻿using SharpGL;
using System;
using System.Drawing;

namespace MineCad
{
    class Box : ICloneable, IMineCadObject
    {
        /* 
         * Константа, необходимая для конвертации RGB цвета [0; 255]
         * в RGB цвет [0.0f; 1.0f].
         */
        private const float colorConversionConstant = byte.MaxValue;
        private Point center = new Point();
        private float sizeX = 1.0f;
        private float sizeY = 1.0f;
        private float sizeZ = 1.0f;

        public Box() {}

        public Box(in Point center, float sizeX, float sizeY, float sizeZ)
        {
            this.center = (Point)center.Clone();
            this.sizeX = (sizeX > 0.0f) ? sizeX : 1.0f;
            this.sizeY = (sizeY > 0.0f) ? sizeY : 1.0f;
            this.sizeZ = (sizeZ > 0.0f) ? sizeZ : 1.0f;
        }

        /* Построение параллелепипеда по 2м точкам его диагонали. */
        public Box(in Point begin, in Point end)
        {
            if (begin.X == end.X && begin.Y == end.Y && begin.Z == end.Z)
            {
                this.center = (Point)center.Clone();
                this.sizeX = (sizeX > 0.0f) ? sizeX : 1.0f;
                this.sizeY = (sizeY > 0.0f) ? sizeY : 1.0f;
                this.sizeZ = (sizeZ > 0.0f) ? sizeZ : 1.0f;
            }
            else
            {
                this.center = Line.GetCenter(begin, end);
                this.sizeX = (Math.Abs(begin.X - end.X) > 0.0f) ? Math.Abs(begin.X - end.X) : 1.0f;
                this.sizeY = (Math.Abs(begin.Y - end.Y) > 0.0f) ? Math.Abs(begin.Y - end.Y) : 1.0f;
                this.sizeZ = (Math.Abs(begin.Z - end.Z) > 0.0f) ? Math.Abs(begin.Z - end.Z) : 1.0f;
            }
        }

        public Point Center
        {
            get
            {
                return (Point)this.center.Clone();
            }

            set
            {
                this.center = (Point)value.Clone();
            }
        }

        public float SizeX
        {
            get
            {
                return this.sizeX;
            }

            set
            {
                if (value > 0.0f)
                {
                    this.sizeX = value;
                }
            }
        }

        public float SizeY
        {
            get
            {
                return this.sizeY;
            }

            set
            {
                if (value > 0.0f)
                {
                    this.sizeY = value;
                }
            }
        }

        public float SizeZ
        {
            get
            {
                return this.sizeZ;
            }

            set
            {
                if (value > 0.0f)
                {
                    this.sizeZ = value;
                }
            }
        }

        public void DrawOutline(OpenGL gl, float width, Color color)
        {
            /* Установка толщины линий. */
            gl.LineWidth(width);

            /* Установка цвета линий. */
            gl.Color(color.R / colorConversionConstant,
                     color.G / colorConversionConstant,
                     color.B / colorConversionConstant);

            /* Отрисовка ребер верхней грани. */
            gl.Begin(OpenGL.GL_LINE_LOOP);

            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);
            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);
            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);
            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);

            gl.End();

            /* Отрисовка ребер нижней грани. */
            gl.Begin(OpenGL.GL_LINE_LOOP);

            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);
            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);
            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);
            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);

            gl.End();

            /* Отрисовка вертикальных ребер. */
            gl.Begin(OpenGL.GL_LINES);

            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);
            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);

            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);
            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);

            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);
            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);

            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);
            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);

            gl.End();
        }

        public void Draw(OpenGL gl, Color color)
        {
            /* Установка цвета параллелепипеда. */
            gl.Color(color.R / colorConversionConstant,
                     color.G / colorConversionConstant,
                     color.B / colorConversionConstant);

            gl.Begin(OpenGL.GL_QUADS);

            /* Отрисовка верхней грани. */
            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);
            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);
            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);
            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);

            /* Отрисовка нижней грани. */
            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);
            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);
            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);
            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);

            gl.End();

            /* Отрисовка боковых граней. */
            gl.Begin(OpenGL.GL_QUAD_STRIP);

            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);
            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);

            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);
            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);

            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);
            gl.Vertex(this.center.X + this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);

            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);
            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z + this.sizeZ / 2.0f);

            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y + this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);
            gl.Vertex(this.center.X - this.sizeX / 2.0f, this.center.Y - this.sizeY / 2.0f, this.center.Z - this.sizeZ / 2.0f);

            gl.End();
        }

        public static void DrawOutline(OpenGL gl, in Point center, float sizeX, float sizeY, float sizeZ, float width, Color color)
        {
            /* Установка толщины линий. */
            gl.LineWidth(width);

            /* Установка цвета линий. */
            gl.Color(color.R / colorConversionConstant,
                     color.G / colorConversionConstant,
                     color.B / colorConversionConstant);

            /* Отрисовка ребер верхней грани. */
            gl.Begin(OpenGL.GL_LINE_LOOP);

            gl.Vertex(center.X - sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z - sizeZ / 2.0f);
            gl.Vertex(center.X + sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z - sizeZ / 2.0f);
            gl.Vertex(center.X + sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z + sizeZ / 2.0f);
            gl.Vertex(center.X - sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z + sizeZ / 2.0f);

            gl.End();

            /* Отрисовка ребер нижней грани. */
            gl.Begin(OpenGL.GL_LINE_LOOP);

            gl.Vertex(center.X - sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z - sizeZ / 2.0f);
            gl.Vertex(center.X - sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z + sizeZ / 2.0f);
            gl.Vertex(center.X + sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z + sizeZ / 2.0f);
            gl.Vertex(center.X + sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z - sizeZ / 2.0f);

            gl.End();

            /* Отрисовка вертикальных ребер. */
            gl.Begin(OpenGL.GL_LINES);

            gl.Vertex(center.X - sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z - sizeZ / 2.0f);
            gl.Vertex(center.X - sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z - sizeZ / 2.0f);

            gl.Vertex(center.X + sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z - sizeZ / 2.0f);
            gl.Vertex(center.X + sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z - sizeZ / 2.0f);

            gl.Vertex(center.X + sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z + sizeZ / 2.0f);
            gl.Vertex(center.X + sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z + sizeZ / 2.0f);

            gl.Vertex(center.X - sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z + sizeZ / 2.0f);
            gl.Vertex(center.X - sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z + sizeZ / 2.0f);

            gl.End();
        }

        public static void Draw(OpenGL gl, in Point center, float sizeX, float sizeY, float sizeZ, Color color)
        {
            /* Установка цвета параллелепипеда. */
            gl.Color(color.R / colorConversionConstant,
                     color.G / colorConversionConstant,
                     color.B / colorConversionConstant);

            gl.Begin(OpenGL.GL_QUADS);

            /* Отрисовка верхней грани. */
            gl.Vertex(center.X - sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z - sizeZ / 2.0f);
            gl.Vertex(center.X + sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z - sizeZ / 2.0f);
            gl.Vertex(center.X + sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z + sizeZ / 2.0f);
            gl.Vertex(center.X - sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z + sizeZ / 2.0f);

            /* Отрисовка нижней грани. */
            gl.Vertex(center.X - sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z - sizeZ / 2.0f);
            gl.Vertex(center.X - sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z + sizeZ / 2.0f);
            gl.Vertex(center.X + sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z + sizeZ / 2.0f);
            gl.Vertex(center.X + sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z - sizeZ / 2.0f);

            gl.End();

            /* Отрисовка боковых граней. */
            gl.Begin(OpenGL.GL_QUAD_STRIP);

            gl.Vertex(center.X - sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z - sizeZ / 2.0f);
            gl.Vertex(center.X - sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z - sizeZ / 2.0f);

            gl.Vertex(center.X + sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z - sizeZ / 2.0f);
            gl.Vertex(center.X + sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z - sizeZ / 2.0f);

            gl.Vertex(center.X + sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z + sizeZ / 2.0f);
            gl.Vertex(center.X + sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z + sizeZ / 2.0f);

            gl.Vertex(center.X - sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z + sizeZ / 2.0f);
            gl.Vertex(center.X - sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z + sizeZ / 2.0f);

            gl.Vertex(center.X - sizeX / 2.0f, center.Y + sizeY / 2.0f, center.Z - sizeZ / 2.0f);
            gl.Vertex(center.X - sizeX / 2.0f, center.Y - sizeY / 2.0f, center.Z - sizeZ / 2.0f);

            gl.End();
        }

        public object Clone()
        {
            return new Box(this.center, this.sizeX, this.sizeY, this.sizeZ);
        }
    }
}